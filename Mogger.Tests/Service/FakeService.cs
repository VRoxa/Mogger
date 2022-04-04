using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogger.Tests.Service;

internal class FakeService
{
    private readonly ILogger<FakeService> _logger;

    public FakeService(ILogger<FakeService> logger)
    {
        _logger = logger;
    }

    public void DoWork()
    {
        _logger.LogInformation("Doing some work...");
    }

    public void DoNothing()
    {
    }
}
