using System;
using System.Collections.Generic;

namespace FocusXamarinMobileApplication.Models;

public static class Visibility
{
    public enum OptionType
    {
        SITE_RISK,
        TIMESHEET,
        DIARY,
        MEASURES,
        UTILITIES_CROSSED,
        LATERAL,
        CALIBRATION,
        PERMIT,
        DOCS,
        PROJECT_SUMMARY,
        VISITORS,
        PLANT,
        IMAGES
    }

    private static readonly Dictionary<OptionType, DaysToShow> VisibilityDict =
        new()
        {
            { OptionType.SITE_RISK, DaysToShow.TODAY },
            { OptionType.TIMESHEET, DaysToShow.TODAY_YESTERDAY },
            { OptionType.DIARY, DaysToShow.TODAY_YESTERDAY },
            { OptionType.MEASURES, DaysToShow.TODAY_YESTERDAY },
            { OptionType.UTILITIES_CROSSED, DaysToShow.TODAY_YESTERDAY },
            { OptionType.LATERAL, DaysToShow.TODAY_BELOW },
            { OptionType.CALIBRATION, DaysToShow.TODAY_BELOW },
            { OptionType.PERMIT, DaysToShow.TODAY },
            { OptionType.DOCS, DaysToShow.ALL },
            { OptionType.PROJECT_SUMMARY, DaysToShow.ALL },
            { OptionType.VISITORS, DaysToShow.TODAY },
            { OptionType.PLANT, DaysToShow.TODAY },
            { OptionType.IMAGES, DaysToShow.TODAY }
        };

    private static readonly Dictionary<OptionType, DaysToShow> VisibilityDictSuper =
        new()
        {
            { OptionType.SITE_RISK, DaysToShow.TODAY },
            { OptionType.TIMESHEET, DaysToShow.TODAY_YESTERDAY },
            { OptionType.DIARY, DaysToShow.TODAY_YESTERDAY },
            { OptionType.MEASURES, DaysToShow.TODAY_YESTERDAY },
            { OptionType.UTILITIES_CROSSED, DaysToShow.TODAY_YESTERDAY },
            { OptionType.LATERAL, DaysToShow.TODAY_BELOW },
            { OptionType.CALIBRATION, DaysToShow.TODAY_BELOW },
            { OptionType.PERMIT, DaysToShow.TODAY },
            { OptionType.DOCS, DaysToShow.ALL },
            { OptionType.PROJECT_SUMMARY, DaysToShow.ALL },
            { OptionType.VISITORS, DaysToShow.TODAY },
            { OptionType.PLANT, DaysToShow.TODAY },
            { OptionType.IMAGES, DaysToShow.TODAY }
        };

    public static bool GetVisibility(OptionType option, Person person, DateTime date)
    {
        //TODO handle person role type
        return IsVisible(option, date);
    }

    private static bool IsVisible(OptionType option, DateTime date)
    {
        var days = VisibilityDict[option];
        var visible = false;
        switch (days)
        {
            case DaysToShow.ALL:
                visible = true;
                break;
            case DaysToShow.TODAY:
                visible = date.Date == DateTime.Today.Date;
                break;
            case DaysToShow.TODAY_YESTERDAY:
                visible = date.Date == DateTime.Today.Date || date.Date == DateTime.Today.Date.AddDays(-1);
                break;
            case DaysToShow.TODAY_BELOW:
                visible = date.Date <= DateTime.Today.Date;
                break;
            default:
                return false;
        }

        return visible;
    }

    private enum DaysToShow
    {
        TODAY,
        TODAY_YESTERDAY,
        TODAY_BELOW,
        ALL
    }
}