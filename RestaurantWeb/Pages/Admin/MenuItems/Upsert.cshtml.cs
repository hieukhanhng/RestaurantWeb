using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace RestaurantWeb.Pages.Admin.MenuItems;
[BindProperties]
public class Upsert : PageModel
{
    public MenuItem MenuItem{ get; set; }
    private readonly IWebHostEnvironment _hostEnvironment;
    public IEnumerable<SelectListItem> CategoryList { get; set; }
    public IEnumerable<SelectListItem> FoodTypeList { get; set; }
    
    private readonly IUnitOfWork _unitOfWork;
    
    public Upsert(IUnitOfWork unitofWork, IWebHostEnvironment hostEnvironment)
    {
        _unitOfWork = unitofWork;
        _hostEnvironment = hostEnvironment;
        MenuItem = new MenuItem();
    }

    public void OnGet(int? id)
    {
        if (id !=null)
        {
            MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id);
        }
        CategoryList = _unitOfWork.Category.GetAll().Select(i=> 
            new SelectListItem()
        {
            Text = i.Name,
            Value = i.Id.ToString()
        });
        FoodTypeList = _unitOfWork.FoodType.GetAll().Select(i=> 
            new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });    }

    public async Task<IActionResult> OnPost()
    {
        string webRootPath = _hostEnvironment.WebRootPath;
        var files = HttpContext.Request.Form.Files;
        if (MenuItem.Id == 0)
        {
            //create
            string fileName_new = Guid.NewGuid().ToString();
            var upload = Path.Combine(webRootPath, @"images/menuItems");
            var extension = Path.GetExtension(files[0].FileName);
            using (var fileStream = new FileStream(Path.Combine(upload, fileName_new + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }

            MenuItem.Image = @"/images/menuItems/" + fileName_new + extension;
            _unitOfWork.MenuItem.Add(MenuItem);
            _unitOfWork.Save();
        }
        else
        {
            //update
            var objFromDb = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == MenuItem.Id);
            if (files.Count > 0)
            {
                string fileName_new = Guid.NewGuid().ToString();
                var upload = Path.Combine(webRootPath, @"images/menuItems");
                var extension = Path.GetExtension(files[0].FileName);
                
                //Delete old image
                var oldImgPath = Path.Combine(webRootPath, objFromDb.Image.TrimStart(Path.DirectorySeparatorChar));
                if (System.IO.File.Exists(oldImgPath))
                {
                    System.IO.File.Delete(oldImgPath);
                }
                
                //Add
                using (var fileStream = new FileStream(Path.Combine(upload, fileName_new + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                MenuItem.Image = @"/images/menuItems/" + fileName_new + extension;
            }
            else
            {
                MenuItem.Image = objFromDb.Image;
            }
            _unitOfWork.MenuItem.Update(MenuItem);
            _unitOfWork.Save();
        }
        return RedirectToPage("./Index");
    }
}