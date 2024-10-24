using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Registration.DataAccess.Repository;
using Registration.DataAccess.Repository.IRepository;
using Registration.Models;
using SQLitePCL;
using System.Diagnostics;

namespace Registration.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitofWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitofWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Candidates> candidatesList = _unitofWork.Candidate.GetAll(includeProperties: "Course"); 
            return View(candidatesList);
        }
        public IActionResult Sas()
        {
           
            return View();
        }
        public IActionResult Details(int? candidateId)
        {
            Candidates candidate= _unitofWork.Candidate.Get(u=>u.Id==candidateId,includeProperties: "Course");
            return View(candidate);
        }



        //public IActionResult Approve(int candidateId)
        //{
        //    var candidate = _unitofWork.Candidate.Get(u => u.Id == candidateId);
        //    if (candidate != null)
        //    {
        //        candidate.Status = "Approved"; // Set status to Approved
        //        _unitofWork.Candidate.Update(candidate);
        //        _unitofWork.Save();
        //    }

        //    return RedirectToAction(nameof(Index)); // Redirect back to the Index action
        //}

        //public IActionResult Deny(int candidateId)
        //{
        //    var candidate = _unitofWork.Candidate.Get(u => u.Id == candidateId);
        //    if (candidate != null)
        //    {
        //        candidate.Status = "Denied"; // Set status to Denied
        //        _unitofWork.Candidate.Update(candidate);
        //        _unitofWork.Save();
        //    }

        //    return RedirectToAction(nameof(Index)); // Redirect back to the Index action
        //}


        [HttpPost]
        public IActionResult HandleCandidateAction(int candidateId, string action)
        {
            var candidate = _unitofWork.Candidate.Get(u => u.Id == candidateId);

            if (candidate != null)
            {
                if (action == "approve")
                {
                    candidate.Status = "Approved"; // Set status to Approved
                }
                else if (action == "deny")
                {
                    candidate.Status = "Denied"; // Set status to Denied
                }

                _unitofWork.Candidate.Update(candidate);
                _unitofWork.Save();
            }

            return RedirectToAction(nameof(Index)); // Redirect back to the Index action
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
