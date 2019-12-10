using System;
using System.Collections.Generic;
using System.Text;

namespace OEE_WPF
{
    public class OEEModel
    {
        public DisponibilityModel disponibility = new DisponibilityModel();
        public QualityModel quality = new QualityModel();
        public PerformanceModel performance = new PerformanceModel();
        public double result = 0;
    }

    public class DisponibilityModel
    {
        public List<Downtime> unexpectedDowntime = new List<Downtime>();
        public List<Downtime> expectedDowntime = new List<Downtime>();
        public int expectedDaysOfWork = 0;
        public double expectedHoursDays = 0;
        public int totalDaysOfWork = 0;
        public double totalHoursOfWork = 0;
        public double expectedWorkedHours = 0;
        public double workedHours = 0;
    }

    public class Downtime
    {
        public String reason = "";
        public double timeDown = 0;
    }

    public class QualityModel
    {
        public double defectItems = 0;
    }

    public class PerformanceModel
    {
        public double expectedProduction = 0;
        public double actualProduction = 0;
        public Production productionSchedule = new Production();
        public List<Production> actualProductionSchedule = new List<Production>();
        public String unit1 = "metros";
        public String unit2 = "segundo";
    }

    public class Production
    {
        public int days = 0;
        public double time = 0;
        public double totalProduced = 0;
        public double productionAmount = 0;
    }
}
