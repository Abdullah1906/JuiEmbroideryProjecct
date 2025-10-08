using JES.DB;
using JES.Models;
using JES.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JES.Controllers
{
    public class OrganogramController : Controller
    {
        private readonly DataContext _context;

        public OrganogramController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new OrganogramViewModel
            {
                FactoryNodes = await _context.OrgNodes
                    .Where(n => n.ChartType == "factory")
                    .OrderBy(n => n.Level)
                    .ThenBy(n => n.DisplayOrder)
                    .ToListAsync(),

                OfficeNodes = await _context.OrgNodes
                    .Where(n => n.ChartType == "office")
                    .OrderBy(n => n.Level)
                    .ThenBy(n => n.DisplayOrder)
                    .ToListAsync(),

                ShiftInfoList = await _context.ShiftInfos
                    .Where(s => s.ChartType == "factory")
                    .ToListAsync()
            };

            return View(viewModel);
        }

        // API endpoint to get nodes dynamically
        [HttpGet]
        public async Task<IActionResult> GetNodes(string chartType)
        {
            var nodes = await _context.OrgNodes
                .Where(n => n.ChartType == chartType)
                .OrderBy(n => n.Level)
                .ThenBy(n => n.DisplayOrder)
                .ToListAsync();

            return Json(nodes);
        }

        // CRUD Operations
        [HttpPost]
        public async Task<IActionResult> CreateNode([FromBody] OrgNode node)
        {
            _context.OrgNodes.Add(node);
            await _context.SaveChangesAsync();
            return Json(new { success = true, id = node.Id });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNode(int id, [FromBody] OrgNode node)
        {
            var existingNode = await _context.OrgNodes.FindAsync(id);
            if (existingNode == null)
                return NotFound();

            existingNode.Title = node.Title;
            existingNode.Name = node.Name;
            existingNode.NodeType = node.NodeType;
            existingNode.Level = node.Level;
            existingNode.DisplayOrder = node.DisplayOrder;

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNode(int id)
        {
            var node = await _context.OrgNodes.FindAsync(id);
            if (node == null)
                return NotFound();

            _context.OrgNodes.Remove(node);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
    }

}
