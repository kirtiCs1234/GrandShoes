using Helper;
using Model;
using Model.Log;
using POS.Controllers;
using POS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.Log)]
    public class LogController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? page)
        {
			//int TotalCount = 0;
			//var pageSize = 10;
			//var pageNumber = page ?? 1;
			//int CurrentPage = pageNumber;
			//var endPage = CurrentPage + 4;
			//int PagesToShow = 10;
			//ViewBag.PageSize = pageSize;
			//var LogModelList = Services.LogService.GetPaging(page, out TotalCount);
			//ViewBag.TotalCount = TotalCount;
			//var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
			//int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
			//ViewBag.result = result;
			//ViewBag.totalPages = totalPages;
			//ViewBag.CurrentPage = CurrentPage;
			//var pageCount = result.Count();
			//ViewBag.pageCount = pageCount;     
			//ViewBag.endPage = endPage;
			var LogModelList = Services.LogService.GetAll();

			return View(LogModelList);
          
        }
        public ActionResult _Index1(LogSearch search, int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var LogModelList = Services.LogService.GetSearchData(search, page, out TotalCount);
            ViewBag.TotalCount = TotalCount;
            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(LogModelList);
           
            
        }
		public ActionResult Download(int id)
		{
			var firstname = "";
			var lastname = "";
			var page = "";
			var actionlogtype = "";
			var log =Services.LogService.GetById(id);
			var s = log.LogTable.Replace("{", "{\r\n\t");
			s = s.Replace(",", "\r\n\t");
			s = s.Replace("}", "\r\n}");
			try { firstname = log.User.FirstName; } catch (Exception) { }
			try { lastname = log.User.LastName; } catch (Exception) { }
			try { page = log.PageName.Page; } catch (Exception) { }
			try { actionlogtype = log.ActionLog.ActionLogType; } catch (Exception) { }
			var str = "User Name: " + firstname + " " + lastname
					+ "\r\nAction: [" + actionlogtype + "] on Page [" + page
					+ "]\r\nTime: [" + log.LogDate + "]\r\n" + s;
			var stream = GenerateStreamFromString(str);
			var downloadFile = new FileStreamResult(stream, "text/plain") { FileDownloadName = "log.txt" };
			return downloadFile;
		}

		public ActionResult View(int id)
		{
			//var session = SessionManagement.CurrentUser;
			
			string page = "";
			string actionlogType = "";
			var log = Services.LogService.GetById(id);
			if (log.PageName != null)
			{
				page = log.PageName.Page;
			}
			if (log.ActionLog != null)
			{
				actionlogType = log.ActionLog.ActionLogType;
			}
			var s = "";
			if (log.LogTable != null)
			{
				try
				{
					s = log.LogTable.Replace("{", "{<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
					s = s.Replace(",", "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
					s = s.Replace("}", "<br>}");
				}
				catch (Exception ex) { s = ""; }
			}

			var str = "User Name: " + log.User.FirstName + " " + log.User.LastName + " and Role: " 
					+ "<br>Action: [" + actionlogType + "] on Page [" + page
					+ "]Time: [" + log.LogDate + "]<br>"
					+ s;
			return Content(str);
		}
		public Stream GenerateStreamFromString(string s)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}
	}
}
