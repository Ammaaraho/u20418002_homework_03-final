using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u20418002_homework_03.Models; //link Models folder

namespace u20418002_homework_03.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet] //Make Index GET Type
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost] // make Index Post Type
        public ActionResult Index(FormCollection form , HttpPostedFileBase file)
        {     
            //check the selected option from form collection
            if (form["optradio"] == "Document")  //if option is document then save to Documents folder in Media folder in Content 
            {
                file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Media/Documents"), file.FileName));
            }
            if (form["optradio"] == "Image")
            {
                file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Media/Images"), file.FileName));
            }
            if (form["optradio"] == "Video")
            {
                file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/Media/Videos"), file.FileName));
            }

            //Finally Redirect back to Index page 
            return RedirectToAction("Index");
        }

        [HttpGet]  //Make About Get Type 
        public ActionResult About()
        {
            return View(); //Return About View
        }

        [HttpGet] // Mkae Files Get Type
        public ActionResult Files()
        {
            List<FileModel> Files = new List<FileModel>(); // Instantiate List to Hold files
          
            foreach (var file in Directory.GetFiles(Server.MapPath("~/Content/Media/Documents")) )
            {
                //Create new FileModel item and add to List of Files
                FileModel File = new FileModel();
                File.FileName = Path.GetFileName(file);
                File.FileType = "Document";
                Files.Add(File);
            }

            // Read all Files in Images Folder
            foreach (var file in Directory.GetFiles(Server.MapPath("~/Content/Media/Images")))
            {
                //Create new FileModel item and add to List of Files
                FileModel File = new FileModel();
                File.FileName = Path.GetFileName(file);
                File.FileType = "Image";
                Files.Add(File);
            }

            // Read all Files in Videos Folder
            foreach (var file in Directory.GetFiles(Server.MapPath("~/Content/Media/Videos")))
            {
                //Create new FileModel item and add to List of Files
                FileModel File = new FileModel();
                File.FileName = Path.GetFileName(file);
                File.FileType = "Video";
                Files.Add(File);
            }

            return View(Files); //Send Files list to Files View
        }

        public FileResult DownloadFile(string fileName ,string  fileType )
        {
            //Check where file esits if found the return the file for download
            if (fileType == "Document" )
            {
                return File(System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Media/Documents/") + fileName), "application/octet-stream", fileName);
            }
            else if (fileType == "Image")
            {
                return File(System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Media/Images/") + fileName), "application/octet-stream", fileName);
            }
            else
            {
                return File(System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Media/Videos/") + fileName), "application/octet-stream", fileName);
            }

        }
        
        public ActionResult DeleteFile(string fileName, string fileType)
        {
            //Check where file esits if found the return the file for download
            if (fileType == "Document")
            {
                System.IO.File.Delete(Server.MapPath("~/Content/Media/Documents/") + fileName);
            }
            else if (fileType == "Image")
            {
                System.IO.File.Delete(Server.MapPath("~/Content/Media/Images/") + fileName);
            }
            else
            {
                System.IO.File.Delete(Server.MapPath("~/Content/Media/Videos/") + fileName);
            }
            //End By Redirecting Back to Index
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Images()
        {
            List<FileModel> Images = new List<FileModel>();
            foreach (var file in Directory.GetFiles(Server.MapPath("~/Content/Media/Images")))
            {
                FileModel locatedFile = new FileModel();
                locatedFile.FileName = Path.GetFileName(file);
                Images.Add(locatedFile);
            }
            return View(Images);
        }

        public FileResult DownloadImage(string fileName)
        {
            byte[] Image = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Media/Images/") + fileName);
            return File(Image, "application/octet-stream", fileName);
        }
      
        public ActionResult DeleteImage(string fileName)
        {
            System.IO.File.Delete(Server.MapPath("~/Content/Media/Images/") + fileName);
            return RedirectToAction("Images");
        }

        [HttpGet]
        public ActionResult Videos()
        {
            List<FileModel> Videos = new List<FileModel>();
            foreach (var file in Directory.GetFiles(Server.MapPath("~/Content/Media/Videos")))
            {
                FileModel locatedFile = new FileModel();
                locatedFile.FileName = Path.GetFileName(file);
                Videos.Add(locatedFile);
            }
            return View(Videos);
        }

        public FileResult DownloadVideo(string fileName)
        {
            byte[] Video = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Media/Videos/") + fileName);
            return File(Video, "application/octet-stream", fileName);
        }
      
        public ActionResult DeleteVideo(string fileName)
        {
            System.IO.File.Delete(Server.MapPath("~/Content/Media/Videos/") + fileName);
            return RedirectToAction("Videos");
        }




    }
}