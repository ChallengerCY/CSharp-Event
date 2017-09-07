using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEvent
{
    


    class Program
    {
        

        static void Main(string[] args)
        {
            //Event是为了通知程序发生了一些事情，这样就能进行下一步处理或者操作
            //事件是基于委托的
            //event是可以被繼承的
            //delegate可以根据需要声明称任意类型的，官方推荐绑定到event上的delegate写法是 返回值为空，带俩个类型参数的 public delegate void ChangedEventHandler(object sender, EventArgs e);,
            //c#自带了已经声明好的处理事件委托
            
            var e = new Event(5);
            e.setValue(10);
            e.onvaluechanged += new Event.OnValueChagned(Event.TiggerEvent);
            e.setValue(20);

            I i = new TestITE();
            i.Test_Event += new DelegateMore(f);
            i.FireAway();
            Console.ReadLine();
        }

        static private void f()
        {
            Console.WriteLine("This is called ");
        }
    }

    class Event
    {
        private int num;
        public delegate void OnValueChagned();
        public event OnValueChagned onvaluechanged;
        public static void TiggerEvent()
        {
            Console.WriteLine("Bind Event");
        }

        public Event(int n)
        {
            setValue(n);
        }

        protected virtual void OnNumChanged()
        {
            if (onvaluechanged != null)
            {
                onvaluechanged();
            }
            else
            {
                Console.WriteLine("no fire");
            }
        }

        public void setValue(int n)
        {
            if (num != n)
            {
                num = n;
                OnNumChanged();
            }
        }
    }

    public delegate void  DelegateMore();

    public interface I{

       event DelegateMore Test_Event;
       void FireAway();
    }

    public class TestITE:I{

        public event DelegateMore Test_Event;

        public void FireAway()
        {
            if (Test_Event != null)
            {
                Test_Event();
            }
        }
    }
}
