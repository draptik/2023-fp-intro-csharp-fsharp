using System.Threading.Tasks.Dataflow;

namespace presentation;

public class IntroDemos
{
   public static double Run2()
   {
      static double ManagerCalc(int a) => a * 2;
      static double CalcSalary(Func<int, double> calc, int salary) => calc(salary);
      var result = CalcSalary(ManagerCalc, 100);
      return result;
   }
}