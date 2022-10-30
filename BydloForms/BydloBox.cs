using BydloForms.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace BydloForms
{
    public class BydloBox
    {
        public BydloBox()
        {
            var props = new PropertySetter<BydloBox>();

            props.Property("Типаж", x => x.Display);
        }

        public IObservable<Display> DisplayProperty => _display;
        public Display Display
        {
            get => _display.Value;
            set => _display.OnNext(value);
        }

        public IObservable<string> Suits => _suits;

        private BehaviorSubject<Display> _display = new(Display.Stack);
        private BehaviorSubject<string> _suits = new("");
    }
}
