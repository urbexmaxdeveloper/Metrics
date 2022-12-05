using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Models.Types;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.DataAccess;
using MetricsAgent;
using NLog;

namespace MetricsAgentTest
{
    public class CpuMetricsControllerTests
    {
        //private CpuMetricsController controller;
        //private Mock<ICpuMetricsDataAdapter> mock;
        //public readonly ILogger<CpuMetricsController> _logger;
        

        //public CpuMetricsControllerTests(ILogger<CpuMetricsController> logger)
        //{
        //    _logger = logger;
        //    mock = new Mock<ICpuMetricsDataAdapter>();
        //}

        //[Fact]
        //public void Create_ShouldCall_Create_From_Repository()
        //{
        //    // Устанавливаем параметр заглушки
        //    // В заглушке прописываем, что в репозиторий прилетитCpuMetric - объект
        //      mock.Setup(repository =>
        //      repository.Create(It.IsAny<CpuMetric>())).Verifiable();
        //    // Выполняем действие на контроллере
        //    var result = controller.Create(new ValueTime
        //    {
        //        Time = TimeSpan.FromSeconds(1),
        //        Value = 50
        //    });
        //    // Проверяем заглушку на то, что пока работал контроллер
        //    // Вызвался метод Create репозитория с нужным типом объекта в параметре
        //    mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()),
        //    Times.AtMostOnce());
        //}
    }
}
