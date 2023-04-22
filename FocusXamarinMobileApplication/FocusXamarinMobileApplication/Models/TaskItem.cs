#region

#endregion

using System;
using FocusXamarinMobileApplication.database;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

public class TaskItem : BusinessEntityBase
{
    [Ignore] public string _category { get; set; } = "";

    public string Category
    {
        get => _category;
        set
        {
            _category = value;

            if (_category.ToLower().Contains("accident"))
            {
                CategoryColour = Color.Red;
                ShowCode = false;
            }
            else if (_category.ToLower().Contains("incident"))
            {
                CategoryColour = Color.Orange;
                ShowCode = false;
            }
            else if (_category.ToLower().Contains("nearmiss"))
            {
                CategoryColour = Color.LightBlue;
                ShowCode = false;
            }
            else if (_category.ToLower().Contains("utility"))
            {
                CategoryColour = Color.Brown;
                ShowCode = false;
            }
            else if (_category.ToLower().Contains("toolbox"))
            {
                CategoryColour = Color.Pink;
                ShowCode = true;
            }
            else
            {
                CategoryColour = Color.Green;
                ShowCode = false;
            }
        }
    }

    public long RemoteTableId { get; set; } = 0;
    public long JobId { get; set; } = 0;
    public long QuoteNumber { get; set; } = 0;

    [Ignore] public DateTime _jobDate { get; set; }

    public DateTime JobDate
    {
        get => _jobDate;
        set
        {
            _jobDate = value;


            if (JobDate.Date == DateTime.Now.Date)
            {
                DateColour = Color.Green;
                Date = "Today";
            }
            else
            {
                DateColour = Color.White;
                Date = JobDate.ToShortDateString();
            }
        }
    }

    public string ProjectName { get; set; } = "";
    public string Description { get; set; } = "";
    public string ClientName { get; set; } = "";
    public string GangLeaderName { get; set; } = "";

    [Ignore] public string _operativesNames { get; set; }

    public string OperativesNames
    {
        get => _operativesNames;
        set => _operativesNames = value;
    }

    public long SupervisorId { get; set; }
    public long GangLeaderId { get; set; }
    public long SubcontractorLabourTeamId { get; set; }
    public long SubContractorCompanyId { get; set; }
    public bool Complete { get; set; } = false;


    [Ignore] public Color _categoryColour { get; set; }

    [Ignore]
    public Color CategoryColour
    {
        get => _categoryColour;
        set => _categoryColour = value;
    }

    [Ignore] public Color _datecolour { get; set; }

    [Ignore]
    public Color DateColour
    {
        get => _datecolour;
        set => _datecolour = value;
    }

    [Ignore] public bool _showCode { get; set; }

    [Ignore]
    public bool ShowCode
    {
        get => _showCode;
        set => _showCode = value;
    }

    [Ignore] public string _date { get; set; }

    [Ignore]
    public string Date
    {
        get => _date;
        set => _date = value;
    }

    public bool DiaryApproved { get; set; } = false;

    public bool LabourApproved { get; set; } = false;

    public bool ClaimesApproved { get; set; } = false;

    public bool LateralsApproved { get; set; }
    public string ContractReference { get; set; }
    public string ContractPrefix { get; set; }
    public string ContractNumber { get; set; }
}