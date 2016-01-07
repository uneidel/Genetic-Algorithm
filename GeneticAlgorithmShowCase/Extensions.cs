using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmShowCase
{
    
        public static class ExtensionMethods
        {

            private static Action EmptyDelegate = delegate () { };
            public static void Refresh(this System.Windows.UIElement uiElement)
            {
                uiElement.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, EmptyDelegate);
            }
        }
    
}
