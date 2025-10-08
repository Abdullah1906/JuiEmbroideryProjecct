using JES.DB;
using JES.Models;
using Microsoft.EntityFrameworkCore;

namespace JES.Seeds
{
    // Data/SeedData.cs

    public static class SeedData
    {
        public static void Initialize(DataContext context)
        {
            // Check if data already exists
            if (!context.OrgNodes.Any(e => e.ChartType == "factory"))
            {
                var factoryNodes = new List<OrgNode>
        {
            // Level 1: SHIFTS (Top)
            new OrgNode
            {
                Title = "SHIFTS",
                Name = "",
                NodeType = "top-level",
                Level = 1,
                DisplayOrder = 1,
                ChartType = "factory"
            },

            // Level 2: SHIFT A and SHIFT B
            new OrgNode
            {
                Title = "SHIFT A",
                Name = "",
                NodeType = "manager-level",
                Level = 2,
                DisplayOrder = 1,
                ChartType = "factory"
            },
            new OrgNode
            {
                Title = "SHIFT B",
                Name = "",
                NodeType = "manager-level",
                Level = 2,
                DisplayOrder = 2,
                ChartType = "factory"
            },

            // ========== SHIFT A Branch ==========
            // Level 3: Shift A - Shift-In-Charge
            new OrgNode
            {
                Title = "SHIFT-IN-CHARGE",
                Name = "Irfan",
                NodeType = "staff-level",
                Level = 3,
                DisplayOrder = 1,
                ChartType = "factory",
                ParentId = 2 // Links to Shift A
            },

            // Level 4: Shift A - Quality
            new OrgNode
            {
                Title = "QUALITY",
                Name = "Mahmud",
                NodeType = "staff-level",
                Level = 4,
                DisplayOrder = 1,
                ChartType = "factory"
            },

            // Level 5: Shift A - Machine-In-Charge
            new OrgNode
            {
                Title = "MACHINE-IN-CHARGE",
                Name = "Ashik",
                NodeType = "staff-level",
                Level = 5,
                DisplayOrder = 1,
                ChartType = "factory"
            },

            // Level 6-8: Shift A - Workers (empty boxes)
            new OrgNode
            {
                Title = "OPERATOR",
                Name = "ABID",
                NodeType = "staff-level",
                Level = 6,
                DisplayOrder = 1,
                ChartType = "factory"
            },
            new OrgNode
            {
                Title = "FRAME",
                Name = "JONY",
                NodeType = "staff-level",
                Level = 7,
                DisplayOrder = 1,
                ChartType = "factory"
            },
            new OrgNode
            {
                Title = "HELPER",
                Name = "JOHN",
                NodeType = "staff-level",
                Level = 8,
                DisplayOrder = 1,
                ChartType = "factory"
            },
             new OrgNode
            {
                Title = "CLEANER",
                Name = "MAXI",
                NodeType = "staff-level",
                Level = 8,
                DisplayOrder = 1,
                ChartType = "factory"
            },

            // ========== SHIFT B Branch ==========
            // Level 3: Shift B - Shift-In-Charge
            new OrgNode
            {
                Title = "SHIFT-IN-CHARGE",
                Name = "NAME",
                NodeType = "staff-level",
                Level = 3,
                DisplayOrder = 2,
                ChartType = "factory",
                ParentId = 3 // Links to Shift B
            },

            // Level 4: Shift B - Quality
            new OrgNode
            {
                Title = "QUALITY",
                Name = "NAME",
                NodeType = "staff-level",
                Level = 4,
                DisplayOrder = 2,
                ChartType = "factory"
            },

            // Level 5: Shift B - Machine-In-Charge
            new OrgNode
            {
                Title = "MACHINE-IN-CHARGE",
                Name = "NAME",
                NodeType = "staff-level",
                Level = 5,
                DisplayOrder = 2,
                ChartType = "factory"
            },

            // Level 6-8: Shift B - Workers (empty boxes)
            new OrgNode
            {
                Title = "OPERATOR",
                Name = "JONY",
                NodeType = "staff-level",
                Level = 6,
                DisplayOrder = 2,
                ChartType = "factory"
            },
            new OrgNode
            {
                Title = "FRAME",
                Name = "HASAN",
                NodeType = "staff-level",
                Level = 7,
                DisplayOrder = 2,
                ChartType = "factory"
            },
            new OrgNode
            {
                Title = "HELPER",
                Name = "A",
                NodeType = "staff-level",
                Level = 8,
                DisplayOrder = 2,
                ChartType = "factory"
            },
              new OrgNode
            {
                Title = "CLEANER",
                Name = "B",
                NodeType = "staff-level",
                Level = 8,
                DisplayOrder = 2,
                ChartType = "factory"
            }
        };
                context.OrgNodes.AddRange(factoryNodes);

            }

            // ========================================
            // OFFICE ORGANOGRAM DATA (Keep as is)
            // ========================================
            if (!context.OrgNodes.Any(e => e.ChartType == "office"))
            {
                var officeNodes = new List<OrgNode>
        {
            // Level 1: Top Management
            new OrgNode
            {
                Title = "List of Office Staff",
                Name = "NAME: A.Z. ZAMAN",
                NodeType = "top-level",
                Level = 1,
                DisplayOrder = 1,
                ChartType = "office"
            },

            // Level 2: Proprietor
            new OrgNode
            {
                Title = "Designation",
                Name = "PROPRIETOR",
                NodeType = "manager-level",
                Level = 2,
                DisplayOrder = 1,
                ChartType = "office"
            },

            // ========== Branch 1 ==========
            new OrgNode
            {
                Title = "MD. KHOKKON",
                Name = "MD. HABIB SIKDER",
                NodeType = "staff-level",
                Level = 3,
                DisplayOrder = 1,
                ChartType = "office"
            },
            new OrgNode
            {
                Title = "MD. SALIM HOSSAIN",
                Name = "",
                NodeType = "staff-level",
                Level = 4,
                DisplayOrder = 1,
                ChartType = "office"
            },
            new OrgNode
            {
                Title = "ACCOUNTANT",
                Name = "",
                NodeType = "staff-level",
                Level = 5,
                DisplayOrder = 1,
                ChartType = "office"
            },

            // ========== Branch 2 ==========
            new OrgNode
            {
                Title = "Shift 7A",
                Name = "",
                NodeType = "staff-level",
                Level = 3,
                DisplayOrder = 2,
                ChartType = "office"
            },
            new OrgNode
            {
                Title = "Shift Incharge",
                Name = "",
                NodeType = "staff-level",
                Level = 4,
                DisplayOrder = 2,
                ChartType = "office"
            },
            new OrgNode
            {
                Title = "Shift 3B",
                Name = "",
                NodeType = "staff-level",
                Level = 5,
                DisplayOrder = 2,
                ChartType = "office"
            },
            new OrgNode
            {
                Title = "Operator",
                Name = "",
                NodeType = "staff-level",
                Level = 6,
                DisplayOrder = 2,
                ChartType = "office"
            },

            // ========== Branch 3 ==========
            new OrgNode
            {
                Title = "Quality",
                Name = "",
                NodeType = "staff-level",
                Level = 3,
                DisplayOrder = 3,
                ChartType = "office"
            },
            new OrgNode
            {
                Title = "Machine In-Charge",
                Name = "",
                NodeType = "staff-level",
                Level = 4,
                DisplayOrder = 3,
                ChartType = "office"
            },
            new OrgNode
            {
                Title = "Operator",
                Name = "",
                NodeType = "staff-level",
                Level = 5,
                DisplayOrder = 3,
                ChartType = "office"
            },

            // ========== Branch 4 ==========
            new OrgNode
            {
                Title = "Frame Man",
                Name = "",
                NodeType = "staff-level",
                Level = 3,
                DisplayOrder = 4,
                ChartType = "office"
            },
            new OrgNode
            {
                Title = "Helper",
                Name = "",
                NodeType = "staff-level",
                Level = 4,
                DisplayOrder = 4,
                ChartType = "office"
            },
            new OrgNode
            {
                Title = "Cleaner",
                Name = "",
                NodeType = "staff-level",
                Level = 5,
                DisplayOrder = 4,
                ChartType = "office"
            }
        };
                context.OrgNodes.AddRange(officeNodes);
            }

            // ========================================
            // SHIFT INFORMATION DATA (Optional - can be removed if not needed)
            // ========================================
            if (context.ShiftInfos.Any())
            {
                return;   // DB has been seeded
            }
            var shiftInfos = new List<ShiftInfo>
        {
            new ShiftInfo
            {
                ShiftName = "Shift A Workers",
                NumberOfWorkers = 10,
                ChartType = "factory"
            },
            new ShiftInfo
            {
                ShiftName = "Shift B Workers",
                NumberOfWorkers = 10,
                ChartType = "factory"
            }
        };

            // Add all data to context
            
            context.ShiftInfos.AddRange(shiftInfos);

            // Save changes to database
            context.SaveChanges();
        }
    }

}
