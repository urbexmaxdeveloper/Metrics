﻿using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgentTest
{
    public class CpuMetricsControllerTests
    {
        private CpuMetricsController _cpuMetricsController;

        public CpuMetricsControllerTests()
        {
            _cpuMetricsController = new CpuMetricsController();
        }

        [Fact]
        public void GetCpuMetrics_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            var result = _cpuMetricsController.GetCpuMetrics(fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }     

    }
}
