using FrontToBack.Models;
using System.Collections.Generic;

namespace FrontToBack.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public SliderContent SliderContent { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public AboutImage AboutImage { get; set; }
        public AboutContent AboutContent { get; set; }
        public List<Expert> Experts { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Florist> Florists { get; set; }
        public List<SocialAddressFlorist> SocialAddressFlorists { get; set; }

    }
}
