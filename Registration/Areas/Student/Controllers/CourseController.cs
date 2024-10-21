using Microsoft.AspNetCore.Mvc;
using Registration.DataAccess.Data;
using Registration.Models;
using Registration.DataAccess.Repository.IRepository;
using Registration.DataAccess.Repository;


namespace Registration.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Course> objCourseList = _unitOfWork.Course.GetAll().ToList();
            return View(objCourseList);
        }
        public IActionResult Create()
        {
            return View(new Course());
        }
        [HttpPost]
        public IActionResult Create(Course obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Course.Add(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Course? courseFromDb = _unitOfWork.Course.Get(u => u.Id == id);
            if (courseFromDb == null)
            {
                return NotFound();
            }
            return View(courseFromDb);

        }

        [HttpPost]
        public IActionResult Edit(Course obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Course.Update(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Course? courseFromDb = _unitOfWork.Course.Get(u => u.Id == id);
            if (courseFromDb == null)
            {
                return NotFound();
            }
            return View(courseFromDb);

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Course? obj = _unitOfWork.Course.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Course.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");


        }
       
        
    }
}
