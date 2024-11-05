using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Registration.DataAccess.Repository;
using Registration.DataAccess.Repository.IRepository;
using Registration.Models;
using SQLitePCL;
using System.Diagnostics;
using System.Security.Claims;

namespace Registration.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitofWork;
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork,UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _unitofWork = unitOfWork;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            IEnumerable<Candidates> candidatesList;
            if (User.IsInRole("Admin"))
            {
                candidatesList = _unitofWork.Candidate.GetAll(includeProperties: "Course").ToList();
                return View(candidatesList);
            }
            candidatesList = _unitofWork.Candidate.GetCandiateCourse(u => u.UserId == userId, includeProperties: "Course");
            return View(candidatesList);
        }
        //public IActionResult Index()
        //{
        //    var userId = _userManager.GetUserId(User);

        //    // Get candidates created by the logged-in user
        //    IEnumerable<Candidates> candidatesList = _unitofWork.Candidate.GetAll(includeProperties: "Course")
        //        .Where(c => c.UserId == userId);

        //    return View(candidatesList);
        //}


        public IActionResult Details(int? candidateId)
        {
            Candidates candidate= _unitofWork.Candidate.Get(u=>u.Id==candidateId,includeProperties: "Course");
            return View(candidate);
        }





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
