using Microsoft.Extensions.Logging;
using MvvmCross.ViewModels;

namespace HelloMvx.Core.ViewModels
{
    public class AboutViewModel : MvxViewModel
    {
        private readonly ILogger<AboutViewModel> _logger;

        public AboutViewModel(
            ILogger<AboutViewModel> logger
        )
        {
            _logger = logger;
        }

    }
}
