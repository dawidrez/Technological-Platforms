using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class NewtonCounter
    {
        public int n;
        public int k;

        public  int Calculate(int mode)
        {
            if(n <1 || k < 0)
            {
                return -1;
            }
            if (n < k)
            {
                return -2;
            }
            int ret=0;
            if (mode==Constants.TASK)
                 ret= CalTasks();
           else
                ret = CalDel();
            return ret;
        }

        private int CalTasks()
        {
            Task<long> counterTask = Task.Factory.StartNew(
                (obj) => CalCounter(),
                100 //state
                );

            Task<long> denominatorTask = Task.Factory.StartNew(
                (obj) => CalDenominator(),
                100 //state
                );

            counterTask.Wait();
            denominatorTask.Wait();
            return (int)(counterTask.Result / denominatorTask.Result);

        }

        private int CalDel()
        {
            Func<long> op = CalCounter;
            IAsyncResult resCounter;
            resCounter = op.BeginInvoke(null,null);
            Func<long> op2 = CalDenominator;
            IAsyncResult resDenomiator;
            resDenomiator = op2.BeginInvoke(null, null);
            while (true)
            {
                if (resCounter.IsCompleted == true && resDenomiator.IsCompleted == true)
                {
                    break;
                }
            }
            var res1 = op.EndInvoke(resCounter);
            var res2 = op2.EndInvoke(resDenomiator);
            return (int)(res1/res2);

        }

        public async Task<int> CalAsync()
        {
            var counter = Task.Run(CalCounter);
            var denominator = Task.Run(CalDenominator);

            await Task.WhenAll(counter, denominator);

            return (int)(counter.Result / denominator.Result);
        }

        private long CalCounter()
        {
            long ret = 1;
            for (int i = n; i >  k; i--)
            {
                ret *= i;

            }
            return ret;
        }

        private long CalDenominator()
        {
            long ret = 1;
            for (int i = 1; i <=n-k; i++)
            {
                ret *= i;

            }
            return ret;
        }


    }
}
