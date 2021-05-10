using System;
using VmTest.Compiler;
using VmTest.ExecutionEngine;

namespace VmTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var cg = new CodeGenerator("20/2");
            var prog = cg.Compile();

            var vm = new VirtualMachine(prog);
            vm.Run();
            Console.WriteLine(vm.R);
        }
    }
}