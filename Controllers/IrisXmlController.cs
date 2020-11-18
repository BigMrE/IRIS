using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using System.Management.Automation;
using IRIS.Models;
//using LCSimple;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Diagnostics;
//using AspNetCore;

namespace IRIS.Controllers
{
    public class IrisXmlController : Controller
    {
        private readonly ORXI_CCMxml _context;

        public IrisXmlController(ORXI_CCMxml context)
        {
            _context = context;
        }
        private void GetModelData()
        {
            ViewBag.IrisXmlLog = this._context.IrisXmlLog.ToList();
        }

        public ActionResult DownloadFile(string item, string OrderNum)
        {
            var Orders = from s in this._context.IrisXmlLog
                         select s.XmlPayload;
            var thise = Orders.ToList();
            var bob = thise[0];
            var bob2 = FormatXml(bob);
            byte[] bytes = Encoding.ASCII.GetBytes(bob2);
            string fileName = OrderNum + ".xml";
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public string FormatXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                // Handle and throw if fatal exception here; don't just ignore them
                return xml;
            }
        }

        public static bool IsValidXml(string xmlString)
        {
            Regex tagsWithData = new Regex("<\\w+>[^<]+</\\w+>");

            //Light checking
            if (string.IsNullOrEmpty(xmlString) || tagsWithData.IsMatch(xmlString) == false)
            {
                return false;
            }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();

                xmlDocument.LoadXml(xmlString);
                return true;
            }
            catch (Exception e1)
            {
                return false;
            }
        }


        // GET: IrisXml
        public async Task<IActionResult> View(int? id, string sortField, string currentSortField, string currentSortOrder, string SearchOrder, string SearchMSGID, int? pageNumber)
        {
            int pageSize = 15;
            ViewData["id"] = id.Value;

            var Orders = from s in this._context.IrisXmlLog
                         select s;
//                        if (!int.   .IsNullOrEmpty(id)) 
//                        { 
                            Orders = Orders.Where(s => s.SequenceNumber.Equals(id));
            //                        }
            var thise = Orders.ToList();
//            var bob = thise[0].XmlPayload;
//            var bob2 = FormatXml(bob);
            thise[0].XmlPayload = FormatXml(thise[0].XmlPayload);
            Orders = thise.AsQueryable();

            return View(Orders);
//            return View(await PaginatedList<IrisXmlLog>.CreateAsync(Orders.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: IrisXml
        public async Task<IActionResult> Details(int? id, string sortField, string currentSortField, string currentSortOrder, string SearchOrder, string SearchMSGID, int? pageNumber)
        {
            int pageSize = 15;
            ViewData["id"] = id.Value;

            var Orders = from s in this._context.IrisXmlLog
                         select s;
            //                        if (!int.   .IsNullOrEmpty(id)) 
            //                        { 
            Orders = Orders.Where(s => s.SequenceNumber.Equals(id));
            //                        }
            //            ViewData["msID"] = "Hi Mom";
            ViewData["bugs"] = "Hi Mom";
            var thise = Orders.ToList();
            //            var bob = thise[0].XmlPayload;
            //            var bob2 = FormatXml(bob);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(thise[0].XmlPayload);
            XmlSchemaSet schema = new XmlSchemaSet();
            //            schema.Add("", "\\apvep60832\g$\GMC1\cache\xsd\DOMS2GMC_IrisLitpackRequest_Final001_7x.xsd");
            //            xmlDoc.Validate(schema, validationEventHandler);

            string xpath = "IRISLitpackRequest";
            XmlNodeList nodes = NewMethod(xmlDoc, xpath);
            //            var nodes = xmlDoc.HasChildNodes(xpath);

            Debug.WriteLine("I am here");
            foreach (XmlNode childrenNode in nodes)
            {
//                XmlNodeList message = XDocument.GetElementsMyTagName("")
                Debug.WriteLine("I am here " + nodes);
                Debug.WriteLine("hello " + childrenNode.Name);
                Debug.WriteLine($"Item #{childrenNode.Name}");
                //ViewData["msID"] = childrenNode.PreviousSibling.Value;
                //            http://localhost:19949/Xml/Details/259908
                ViewData["msgDatetime"] = childrenNode.SelectSingleNode("message/msgDatetime").InnerText;
                ViewData["msgControlId"] = childrenNode.SelectSingleNode("message/msgControlId").InnerText;
                ViewData["msId"] = childrenNode.SelectSingleNode("message/msId").InnerText;
                ViewData["source"] = childrenNode.SelectSingleNode("message/source").InnerText;
                ViewData["printQueue"] = childrenNode.SelectSingleNode("message/printQueue").InnerText;
                ViewData["documentType"] = childrenNode.SelectSingleNode("message/documentTypes/documentType").InnerText;
                // patient info
                ViewData["accountNumber"] = childrenNode.SelectSingleNode("patient/accountNumber").InnerText;
                ViewData["altAccountNumber"] = childrenNode.SelectSingleNode("patient/altAccountNumber").InnerText;
                ViewData["patientId"] = childrenNode.SelectSingleNode("patient/patientId").InnerText;
                ViewData["FirstName"] = childrenNode.SelectSingleNode("patient/patientFirstName").InnerText;
                ViewData["MiddleName"] = childrenNode.SelectSingleNode("patient/patientMiddleName").InnerText;
                ViewData["LastName"] = childrenNode.SelectSingleNode("patient/patientLastName").InnerText;
                ViewData["AddressLine1"] = childrenNode.SelectSingleNode("patient/patientAddressLine1").InnerText;
                ViewData["AddressLine2"] = childrenNode.SelectSingleNode("patient/patientAddressLine2").InnerText;
                ViewData["City"] = childrenNode.SelectSingleNode("patient/patientCity").InnerText;
                ViewData["State"] = childrenNode.SelectSingleNode("patient/patientState").InnerText;
                ViewData["Zip"] = childrenNode.SelectSingleNode("patient/patientZip").InnerText;
                ViewData["dob"] = childrenNode.SelectSingleNode("patient/dob").InnerText;
                ViewData["gender"] = childrenNode.SelectSingleNode("patient/gender").InnerText;
                ViewData["languageId"] = childrenNode.SelectSingleNode("patient/languageId").InnerText;
                ViewData["Phone"] = childrenNode.SelectSingleNode("patient/patientPhone").InnerText;
                ViewData["Email"] = childrenNode.SelectSingleNode("patient/patientEmail").InnerText;
                ViewData["PreferenceHffEnrollmentFlag"] = childrenNode.SelectSingleNode("patient/patientPreferenceHffEnrollmentFlag").InnerText;
                ViewData["MOHistoryFirstOrderFlag"] = childrenNode.SelectSingleNode("patient/patientMOHistoryFirstOrderFlag").InnerText;
                // patient reorders

            }
            thise[0].XmlPayload = FormatXml(thise[0].XmlPayload);
            thise[0].newItem = FormatXml(thise[0].XmlPayload);
            Orders = thise.AsQueryable();

            return View(Orders);
            //            return View(await PaginatedList<IrisXmlLog>.CreateAsync(Orders.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        private static XmlNodeList NewMethod(XmlDocument xmlDoc, string xpath)
        {
            return xmlDoc.SelectNodes(xpath);
        }

        // GET: IrisXml
        public async Task<IActionResult> Index(string id, string sortField, string currentSortField, string currentSortOrder, string SearchOrder, string SearchMSGID, int? pageNumber)
        {
            int pageSize = 15;
            if (id != null) 
            {
                SearchOrder = id;
            }
            ViewData["OrderFilter"] = SearchOrder;
            ViewData["MSGIDFilter"] = SearchMSGID;
            ViewData["MSGIDSortParm"] = sortField == "MSGID_asc" ? "MSGID_desc" : "MSGID_asc";
            ViewData["OrderIdSortParm"] = sortField == "OrderId_asc" ? "OrderId_desc" : "OrderId_asc";
            ViewData["XmlDataSortParm"] = sortField == "XmlData_asc" ? "XmlData_desc" : "XmlData_asc";
            ViewData["DateTimeSortParm"] = sortField == "DateTime_asc" ? "DateTime_desc" : "DateTime_asc";

            List<IrisXmlLog> IrisXmlLogs = this._context.IrisXmlLog.ToList();
            
            var Orders = from s in this._context.IrisXmlLog
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
            if (!String.IsNullOrEmpty(SearchOrder) || !String.IsNullOrEmpty(SearchMSGID))
            {
                Orders = Orders.Where(s => s.OrderID.Contains(SearchOrder)
                                       || s.MSGID.Contains(SearchMSGID));
            }

            //var Orders2 = PaginatedList<IrisXmlLogs, count, pageNumber, pageSize>;
            //if (!String.IsNullOrEmpty(SearchString))
            //{
            //  Orders = IrisXmlLog.fill(s => s.FillerOrderNum.Contains(SearchString));  
            //}
            if (sortField == null) 
                {
                sortField = "a_a";
                }
            var columnName = sortField.Split("_")[0];
            switch (columnName)
            {
                case "MSID":
                    if (sortField.Split("_")[1] == "desc") {
                        Orders = Orders.OrderByDescending(s => s.MSGID);
                    } 
                    else {
                        Orders = Orders.OrderBy(s => s.MSGID);
                    }
                    break;
                default:
                    Orders = Orders.OrderByDescending(s => s.MSGID);
                    break;
            }
            var thise = Orders.ToList();
            //            thise.ForEach(formatit);
            //            IList<Orders> _temp = new List<Orders>();
            //            var s = new Orders.;
//            var path = new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            XmlSchemaSet schema = new XmlSchemaSet();
            schema.Add("", "\\\\apvep60832\\g$\\GMC1\\cache\\xsd\\DOMS2GMC_IrisLitpackRequest_Final001_7x.xsd");
            //            XmlReader rd = XmlReader.Create(path + "\\input.xml");
            //            XDocument doc = XDocument.Load(rd);
            //            doc.Validate(schema, ValidationEventHandler);
            Console.WriteLine("Schema: " + schema);
//            foreach (var p in Orders) 
//            {
//                //var XmlPayload2 = p.XmlPayload;
//                var XmlPayload2 = FormatXml(p.XmlPayload);
//                Console.WriteLine("XmlPayload2: " + XmlPayload2);
//                MemoryStream stream = new MemoryStream();
//                StreamWriter writer = new StreamWriter(stream);
//                writer.Write(XmlPayload2);
////                writer.Flush();
//                XDocument doc = XDocument.Load(stream);
//                doc.Validate(schema, ValidationEventHandler);

////                XmlReaderSettings booksSettings = new XmlReaderSettings();
////                booksSettings.Schemas.Add("G:\\temp", "G:\\temp\\DOMS2GMC_IrisLitpackRequest_Final001_7x.xsd");
//////                booksSettings.Schemas.Add("\\\\apvep60832\\g$\\GMC1\\cache\\xsd\\", "DOMS2GMC_IrisLitpackRequest_Final001_7x.xsd");
////                booksSettings.ValidationType = ValidationType.Schema;
////                booksSettings.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);

////                XmlReader books = XmlReader.Create("books.xml", booksSettings);

////                while (books.Read()) { }

////                Orders.XmlPayload2.Add(new XmlPayload2());
//            }
            
            foreach ( var item in thise)
            {
                count++;
                Console.WriteLine($"Item #{count}: {item}");
                //                _temp.Add(new Orders
                //                {
                //                    XmlPayload = item.XmlPayload
                //                })
            }
            //                {
            //            var bob = s.XmlPayload;
            //            var bob2 = FormatXml(bob);
            //                s.XmlPayload = FormatXml(s.XmlPayload);
            //            }
            //            Orders = thise.AsQueryable();
            void formatit(string s)
            {
                Console.WriteLine(s);
//                s.XmlPayload = FormatXml(s.XmlPayload);
            }
            return View(await PaginatedList<IrisXmlLog>.CreateAsync(Orders.AsNoTracking(), pageNumber ?? 1, pageSize));
//            return View(await _context.IrisXmlLog.ToListAsync());
        }


        static void validationEventHandler(object sender, ValidationEventArgs e)
        {
            XmlSeverityType type = XmlSeverityType.Warning;
            if (Enum.TryParse<XmlSeverityType>("Error", out type))
            {
                if (type == XmlSeverityType.Error) throw new Exception(e.Message);
            }
        }



        // GET: IrisProcessed/Details/5
        static void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Console.Write("WARNING: ");
                Console.WriteLine(e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Console.Write("ERROR: ");
                Console.WriteLine(e.Message);
            }
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
        public async Task<IActionResult> Create([Bind("SequenceId,FillerOrderNum,MsgControlId,Submissiontime,Durationtime,AccountNumber,AltAccountNumber,PatientId,HealthPlanName,LogoFile,RxpharmCode,HttpReturnCode,Status,Detailedjobstate,OrderControl,OrgCode,DocumentType,Jobidentifier,Jobspoolslot,Queuesapplied,Numberofdocuments,Jobpagecount,PctTno,PctPdf,PctPrintHvs,PctPrintApsUp,PctPrintApsDown,CountGiveCode,CountNewRx,ReorderLineCt,OrderLineCt,PrintAnywherePrinter,Msid,PrefEmail,ActiveJobs,FirstName,LastName,StrDocTypes,PrefDate,JobSource,Odtype,PrintDrop,Edrop,DocsStatusUrl,State,Tfn,DoctorOrderFlag,Age,Gender,Estorage,PctPrintApsDuplex,PatientPreferenceHffEnrollmentFlag,PatientMohistoryFirstOrderFlag,LanguageId,CounthffStatus")] IrisXmlLog IrisXmlLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(IrisXmlLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(IrisXmlLog);
        }

        // GET: IrisProcessed/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var IrisXmlLog = await _context.IrisXmlLog.FindAsync(id);
            if (IrisXmlLog == null)
            {
                return NotFound();
            }
            return View(IrisXmlLog);
        }

        // POST: IrisProcessed/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SequenceId,FillerOrderNum,MsgControlId,Submissiontime,Durationtime,AccountNumber,AltAccountNumber,PatientId,HealthPlanName,LogoFile,RxpharmCode,HttpReturnCode,Status,Detailedjobstate,OrderControl,OrgCode,DocumentType,Jobidentifier,Jobspoolslot,Queuesapplied,Numberofdocuments,Jobpagecount,PctTno,PctPdf,PctPrintHvs,PctPrintApsUp,PctPrintApsDown,CountGiveCode,CountNewRx,ReorderLineCt,OrderLineCt,PrintAnywherePrinter,Msid,PrefEmail,ActiveJobs,FirstName,LastName,StrDocTypes,PrefDate,JobSource,Odtype,PrintDrop,Edrop,DocsStatusUrl,State,Tfn,DoctorOrderFlag,Age,Gender,Estorage,PctPrintApsDuplex,PatientPreferenceHffEnrollmentFlag,PatientMohistoryFirstOrderFlag,LanguageId,CounthffStatus")] IrisXmlLog IrisXmlLog)
        {
            if (id != IrisXmlLog.SequenceNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(IrisXmlLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IrisXmlLogExists(IrisXmlLog.SequenceNumber))
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
            return View(IrisXmlLog);
        }

        // GET: IrisProcessed/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var IrisXmlLog = await _context.IrisXmlLog
                .FirstOrDefaultAsync(m => m.SequenceNumber == id);
            if (IrisXmlLog == null)
            {
                return NotFound();
            }

            return View(IrisXmlLog);
        }

        // POST: IrisProcessed/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var IrisXmlLog = await _context.IrisXmlLog.FindAsync(id);
            _context.IrisXmlLog.Remove(IrisXmlLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IrisXmlLogExists(int id)
        {
            return _context.IrisXmlLog.Any(e => e.SequenceNumber == id);
        }
    }
}
