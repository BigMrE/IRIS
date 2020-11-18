using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IRIS.Models;
//using LCSimple;
using IRIS;
using System.Data.Common;
//using AspNetCore;

namespace IRIS.Controllers
{
    public class IrisProcessedController : Controller
    {
        private readonly ORXI_CCMContext _context;

        public IrisProcessedController(ORXI_CCMContext context)
        {
            _context = context;
        }
        private void GetModelData()
        {
            ViewBag.IrisProcessedLog = this._context.IrisProcessedLog.ToList();
        }
        // GET: IrisProcessed
        public async Task<IActionResult> Index(string sortField, string currentSortField, string currentSortOrder, string SearchOrder, string SearchPatientId, int? pageNumber)
        {
            int pageSize = 15;

            ViewData["OrderFilter"] = SearchOrder;
            ViewData["PatientFilter"] = SearchPatientId;
            ViewData["OrderNumSortParm"] = sortField == "OrderNum_asc" ? "OrderNum_desc" : "OrderNum_asc";
                       ViewData["SubmissiontimeSortParm"] = sortField == "Submissiontime_asc" ? "Submissiontime_desc" : "Submissiontime_asc";
                       ViewData["PatientIdSortParm"] = sortField == "PatientId_asc" ? "PatientId_desc" : "PatientId_asc";
                       ViewData["LogoFileSortParm"] = sortField == "LogoFile_asc" ? "LogoFile_desc" : "LogoFile_asc";
                       ViewData["RxpharmCodeSortParm"] = sortField == "RxpharmCode_asc" ? "RxpharmCode_desc" : "RxpharmCode_asc";
                       ViewData["OrgCodeSortParm"] = sortField == "OrgCode_asc" ? "OrgCode_desc" : "OrgCode_asc";
                       ViewData["OrderControlSortParm"] = sortField == "OrderControl_asc" ? "OrderControl_desc" : "OrderControl_asc";
                       ViewData["DocumentTypeSortParm"] = sortField == "DocumentType_asc" ? "DocumentType_desc" : "DocumentType_asc";
                       ViewData["StrDocTypesSortParm"] = sortField == "StrDocTypes_asc" ? "StrDocTypes_desc" : "StrDocTypes_asc";
                       ViewData["CountGiveCodeSortParm"] = sortField == "CountGiveCode_asc" ? "CountGiveCode_desc" : "CountGiveCode_asc";
                       ViewData["CountNewRxSortParm"] = sortField == "CountNewRx_asc" ? "CountNewRx_desc" : "CountNewRx_asc";
                       ViewData["ReorderLineCtSortParm"] = sortField == "ReorderLineCt_asc" ? "ReorderLineCt_desc" : "ReorderLineCt_asc";
                       ViewData["OrderLineCtSortParm"] = sortField == "OrderLineCt_asc" ? "OrderLineCt_desc" : "OrderLineCt_asc";
                       ViewData["PrintDropSortParm"] = sortField == "PrintDrop_asc" ? "PrintDrop_desc" : "PrintDrop_asc";
                       ViewData["EdropSortParm"] = sortField == "Edrop_asc" ? "Edrop_desc" : "Edrop_asc";
                       ViewData["LanguageIdSortParm"] = sortField == "LanguageId_asc" ? "LanguageId_desc" : "LanguageId_asc";

            List<IrisProcessedLog> irisProcessedLogs = this._context.IrisProcessedLog.ToList();
            
            var Orders = from s in this._context.IrisProcessedLog
                         select s;
            if (pageNumber == null)
            {
                pageNumber = 1;
            }
            var count = await Orders.CountAsync() / pageSize + 1;
            //DateTime desc
            ViewData["currentPage"] = pageNumber;
            ViewData["prevPage"] = pageNumber > 1 ? pageNumber - 1 : null;
            ViewData["PageLink01"] = pageNumber - 5;
            ViewData["PageLink02"] = pageNumber - 4;
            ViewData["PageLink03"] = pageNumber - 3;
            ViewData["PageLink04"] = pageNumber - 2;
            ViewData["PageLink05"] = pageNumber - 1;
            ViewData["PageLink06"] = pageNumber - 0;
            ViewData["PageLink07"] = pageNumber + 1;
            ViewData["PageLink08"] = pageNumber + 2;
            ViewData["PageLink09"] = pageNumber + 3;
            ViewData["PageLink10"] = pageNumber + 4;
            ViewData["PageLink11"] = pageNumber + 5;
            ViewData["nextPage"] = pageNumber < count ? (pageNumber + 1) : null;
            ViewData["count"] = count;
            if (pageNumber < 6)
            {
                ViewData["PageLink01"] = 1;
                ViewData["PageLink02"] = 2;
                ViewData["PageLink03"] = 3;
                ViewData["PageLink04"] = 4;
                ViewData["PageLink05"] = 5;
                ViewData["PageLink06"] = 6;
                ViewData["PageLink07"] = 7;
                ViewData["PageLink08"] = 8;
                ViewData["PageLink09"] = 9;
                ViewData["PageLink10"] = 10;
                ViewData["PageLink11"] = 11;
            }
            if (pageNumber > (count - 5))
            {
                ViewData["PageLink01"] = (count - 10);
                ViewData["PageLink02"] = (count - 9);
                ViewData["PageLink03"] = (count - 8);
                ViewData["PageLink04"] = (count - 7);
                ViewData["PageLink05"] = (count - 6);
                ViewData["PageLink06"] = (count - 5);
                ViewData["PageLink07"] = (count - 4);
                ViewData["PageLink08"] = (count - 3);
                ViewData["PageLink09"] = (count - 2);
                ViewData["PageLink10"] = (count - 1);
                ViewData["PageLink11"] = (count - 0);
            }
            if (!String.IsNullOrEmpty(SearchOrder) || !String.IsNullOrEmpty(SearchPatientId))
            {
                Orders = Orders.Where(s => s.FillerOrderNum.Contains(SearchOrder)
                                       || s.PatientId.Contains(SearchPatientId));
            }


            //           if (!String.IsNullOrEmpty(SearchString))
            //           {
            //               Orders = IrisProcessedLog.fill(s => s.FillerOrderNum.Contains(SearchString));  
            //           }
            if (sortField == null) 
                {
                sortField = "a_a";
                }
            var columnName = sortField.Split("_")[0];
            switch (columnName)
            {
                case "OrderNum":
                    if (sortField.Split("_")[1] == "desc") {
                        Orders = Orders.OrderByDescending(s => s.FillerOrderNum);
                    } 
                    else {
                        Orders = Orders.OrderBy(s => s.FillerOrderNum);
                    }
                    break;
                case "PatientId":
                    if (sortField.Split("_")[1] == "desc") {
                        Orders = Orders.OrderByDescending(s => s.PatientId);
                    }
                    else {
                        Orders = Orders.OrderBy(s => s.PatientId);
                    }
                    break;
                case "LogoFile":
                    if (sortField.Split("_")[1] == "desc")
                    {
                        Orders = Orders.OrderByDescending(s => s.LogoFile);
                    }
                    else
                    {
                        Orders = Orders.OrderBy(s => s.LogoFile);
                    }
                    break;
                case "RxpharmCode":
                    if (sortField.Split("_")[1] == "desc")
                    {
                        Orders = Orders.OrderByDescending(s => s.RxpharmCode);
                    }
                    else
                    {
                        Orders = Orders.OrderBy(s => s.RxpharmCode);
                    }
                    break;
                case "OrgCode":
                    if (sortField.Split("_")[1] == "desc")
                    {
                        Orders = Orders.OrderByDescending(s => s.OrgCode);
                    }
                    else
                    {
                        Orders = Orders.OrderBy(s => s.OrgCode);
                    }
                    break;
                case "OrderControl":
                    if (sortField.Split("_")[1] == "desc")
                    {
                        Orders = Orders.OrderByDescending(s => s.OrderControl);
                    }
                    else
                    {
                        Orders = Orders.OrderBy(s => s.OrderControl);
                    }
                    break;
                case "DocumentType":
                    if (sortField.Split("_")[1] == "desc")
                    {
                        Orders = Orders.OrderByDescending(s => s.DocumentType);
                    }
                    else
                    {
                        Orders = Orders.OrderBy(s => s.DocumentType);
                    }
                    break;
                case "StrDocTypes":
                    if (sortField.Split("_")[1] == "desc")
                    {
                        Orders = Orders.OrderByDescending(s => s.StrDocTypes);
                    }
                    else
                    {
                        Orders = Orders.OrderBy(s => s.StrDocTypes);
                    }
                    break;
                case "CountGiveCode":
                    if (sortField.Split("_")[1] == "desc")
                    {
                        Orders = Orders.OrderByDescending(s => s.CountGiveCode);
                    }
                    else
                    {
                        Orders = Orders.OrderBy(s => s.CountGiveCode);
                    }
                    break;
                case "CountNewRx":
                    if (sortField.Split("_")[1] == "desc")
                    {
                        Orders = Orders.OrderByDescending(s => s.CountNewRx);
                    }
                    else
                    {
                        Orders = Orders.OrderBy(s => s.CountNewRx);
                    }
                    break;
                case "ReorderLineCt":
                    if (sortField.Split("_")[1] == "desc")
                    {
                        Orders = Orders.OrderByDescending(s => s.ReorderLineCt);
                    }
                    else
                    {
                        Orders = Orders.OrderBy(s => s.ReorderLineCt);
                    }
                    break;
                case "OrderLineCt":
                    if (sortField.Split("_")[1] == "desc")
                    {
                        Orders = Orders.OrderByDescending(s => s.OrderLineCt);
                    }
                    else
                    {
                        Orders = Orders.OrderBy(s => s.OrderLineCt);
                    }
                    break;
                case "PrintDrop":
                    if (sortField.Split("_")[1] == "desc")
                    {
                        Orders = Orders.OrderByDescending(s => s.PrintDrop);
                    }
                    else
                    {
                        Orders = Orders.OrderBy(s => s.PrintDrop);
                    }
                    break;
                case "Edrop":
                    if (sortField.Split("_")[1] == "desc")
                    {
                        Orders = Orders.OrderByDescending(s => s.Edrop);
                    }
                    else
                    {
                        Orders = Orders.OrderBy(s => s.Edrop);
                    }
                    break;
                case "LanguageId":
                    if (sortField.Split("_")[1] == "desc")
                    {
                        Orders = Orders.OrderByDescending(s => s.LanguageId);
                    }
                    else
                    {
                        Orders = Orders.OrderBy(s => s.LanguageId);
                    }
                    break;
                default:
                    Orders = Orders.OrderByDescending(s => s.Submissiontime);
                    break;
            }
            return View(await PaginatedList<IrisProcessedLog>.CreateAsync(Orders.AsNoTracking(), pageNumber ?? 1, pageSize));
//            return View(await _context.IrisProcessedLog.ToListAsync());
        }

        // GET: IrisProcessed/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var irisProcessedLog = await _context.IrisProcessedLog
                .FirstOrDefaultAsync(m => m.SequenceId == id);
            if (irisProcessedLog == null)
            {
                return NotFound();
            }

            return View(irisProcessedLog);
        }

        // GET: IrisProcessed/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: IrisProcessed/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SequenceId,FillerOrderNum,MsgControlId,Submissiontime,Durationtime,AccountNumber,AltAccountNumber,PatientId,HealthPlanName,LogoFile,RxpharmCode,HttpReturnCode,Status,Detailedjobstate,OrderControl,OrgCode,DocumentType,Jobidentifier,Jobspoolslot,Queuesapplied,Numberofdocuments,Jobpagecount,PctTno,PctPdf,PctPrintHvs,PctPrintApsUp,PctPrintApsDown,CountGiveCode,CountNewRx,ReorderLineCt,OrderLineCt,PrintAnywherePrinter,Msid,PrefEmail,ActiveJobs,FirstName,LastName,StrDocTypes,PrefDate,JobSource,Odtype,PrintDrop,Edrop,DocsStatusUrl,State,Tfn,DoctorOrderFlag,Age,Gender,Estorage,PctPrintApsDuplex,PatientPreferenceHffEnrollmentFlag,PatientMohistoryFirstOrderFlag,LanguageId,CounthffStatus")] IrisProcessedLog irisProcessedLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(irisProcessedLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(irisProcessedLog);
        }

        // GET: IrisProcessed/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var irisProcessedLog = await _context.IrisProcessedLog.FindAsync(id);
            if (irisProcessedLog == null)
            {
                return NotFound();
            }
            return View(irisProcessedLog);
        }

        // POST: IrisProcessed/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SequenceId,FillerOrderNum,MsgControlId,Submissiontime,Durationtime,AccountNumber,AltAccountNumber,PatientId,HealthPlanName,LogoFile,RxpharmCode,HttpReturnCode,Status,Detailedjobstate,OrderControl,OrgCode,DocumentType,Jobidentifier,Jobspoolslot,Queuesapplied,Numberofdocuments,Jobpagecount,PctTno,PctPdf,PctPrintHvs,PctPrintApsUp,PctPrintApsDown,CountGiveCode,CountNewRx,ReorderLineCt,OrderLineCt,PrintAnywherePrinter,Msid,PrefEmail,ActiveJobs,FirstName,LastName,StrDocTypes,PrefDate,JobSource,Odtype,PrintDrop,Edrop,DocsStatusUrl,State,Tfn,DoctorOrderFlag,Age,Gender,Estorage,PctPrintApsDuplex,PatientPreferenceHffEnrollmentFlag,PatientMohistoryFirstOrderFlag,LanguageId,CounthffStatus")] IrisProcessedLog irisProcessedLog)
        {
            if (id != irisProcessedLog.SequenceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(irisProcessedLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IrisProcessedLogExists(irisProcessedLog.SequenceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(irisProcessedLog);
        }

        // GET: IrisProcessed/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var irisProcessedLog = await _context.IrisProcessedLog
                .FirstOrDefaultAsync(m => m.SequenceId == id);
            if (irisProcessedLog == null)
            {
                return NotFound();
            }

            return View(irisProcessedLog);
        }

        // POST: IrisProcessed/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var irisProcessedLog = await _context.IrisProcessedLog.FindAsync(id);
            _context.IrisProcessedLog.Remove(irisProcessedLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IrisProcessedLogExists(int id)
        {
            return _context.IrisProcessedLog.Any(e => e.SequenceId == id);
        }
    }
}
