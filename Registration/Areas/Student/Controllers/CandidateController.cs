using Microsoft.AspNetCore.Mvc;
using Registration.DataAccess.Data;
using Registration.Models;
using Registration.DataAccess.Repository.IRepository;
using Registration.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Registration.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;


namespace Registration.Areas.Student.Controllers
{
    [Area("Student")]
    public class CandidateController : Controller

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CandidateController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {

            List<Candidates> objCandidateList = _unitOfWork.Candidate.GetAll(includeProperties: "Course").ToList();

            return View(objCandidateList);
        }
        
        public IActionResult Upsert(int? id)
        {

            //ViewBag.CourseList = CourseList;

            CandidatesVM candidatesVm = new()
            {
                CourseList = _unitOfWork.Course.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Candidate = new Candidates()
            };
            if (id == null || id == 0)
            {
                return View(candidatesVm);
            }
            else
            {
                candidatesVm.Candidate = _unitOfWork.Candidate.Get(u => u.Id == id);
                return View(candidatesVm);
            }


        }
        [HttpPost]
        [HttpPost]
        public IActionResult Upsert(CandidatesVM candidateVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string candidatePath = Path.Combine(wwwRootPath, @"images\Candidates");

                // Ensure the directory exists
                if (!Directory.Exists(candidatePath))
                {
                    Directory.CreateDirectory(candidatePath);
                }

                if (file != null && file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    if (!string.IsNullOrEmpty(candidateVM.Candidate.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, candidateVM.Candidate.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(candidatePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    candidateVM.Candidate.ImageUrl = @"\images\Candidates\" + fileName; // Corrected path
                }

                if (candidateVM.Candidate.Id == 0)
                {
                    _unitOfWork.Candidate.Add(candidateVM.Candidate);
                }
                else
                {
                    _unitOfWork.Candidate.Update(candidateVM.Candidate);
                }
                Console.WriteLine("updated successufflly");
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }

            // If we reach here, something went wrong, so we need to re-populate the CourseList
            candidateVM.CourseList = _unitOfWork.Course.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(candidateVM);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Candidates? candidateFromDb = _unitOfWork.Candidate.Get(u => u.Id == id);
            if (candidateFromDb == null)
            {
                return NotFound();
            }
            return View(candidateFromDb);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            try
            {
                Candidates? obj = _unitOfWork.Candidate.Get(u => u.Id == id);
                if (obj == null)
                {
                    return NotFound();
                }

                _unitOfWork.Candidate.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred while deleting the category.";
                return RedirectToAction("Index");
            }
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Candidates> objCandidateList = _unitOfWork.Candidate.GetAll(includeProperties: "Course").ToList();
            return Json(new {data = objCandidateList});
        }
        #endregion
    }
}
