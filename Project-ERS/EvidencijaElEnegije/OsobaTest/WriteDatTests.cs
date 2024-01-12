using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using EvidencijaElEnegije.PrognoziraneelOstvarenePotrosnje;
using EvidencijaElEnegije.Klase;
using EvidencijaElEnegije.PrognoziranelOstvarenePotrosnje;
using System.Xml.Linq;
using System.IO;
using EvidencijaElEnegije.Meni;

namespace EvidencijaElEnegije.PrognoziraneIOstvarenePotrosnje.Tests
{
    [TestFixture]
    public class WriteDatTests
    {
        [Test]
        public void ProveraIspravnosti_WhenValidInput_ShouldReturnTrue()
        {

            ReadDat writeHendler = new ReadDat();
            DateTime currentLocalTime = DateTime.Now;

      
            Dan validDan = new Dan(17, 9, 2002, true);

            string path = "TestPath";
            string datoteka = "TestDatoteka";

  
            bool result = writeHendler.proveraIspravnosti(validDan, path, datoteka);

     
            Assert.IsTrue(result);
        }


        [Test]
        public void PogresanUnos()
        {
            ReadDat writeHandler = new ReadDat();

       
            Dan invalidDan = new Dan(17, 13, 2202, true);

         


            Assert.Throws<ArgumentException>(() => writeHandler.proveraIspravnosti(invalidDan, null, null));
        }


        [Test]
        public void TestConsoleOutputForZeroInput()
        {
   
            TextWriter originalConsoleOut = Console.Out;

            try
            {
       
                using (var consoleOutput = new StringWriter())
                {
          
                    Console.SetOut(consoleOutput);

                    odabirDatoteke(0);

                    Assert.IsTrue(consoleOutput.ToString().Contains("Prvo morate ispisati sve datoteke kako bi se za svaku generisao redni broj"));
                }
            }
            finally
            {
              
                Console.SetOut(originalConsoleOut);
            }
        }




        private void odabirDatoteke(int input)
        {
            ReadDat readHandler = new ReadDat();
            readHandler.odabirDatoteka(input);
        }
    }

}