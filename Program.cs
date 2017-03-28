using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Accord.Neuro;
using Accord.Neuro.Networks;
using Accord.Neuro.Learning;
using Accord.Neuro.ActivationFunctions;
using Accord.Math;

using Accord.Imaging;
using Accord.Imaging.Filters;


namespace facecrop
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] _files = System.IO.Directory.GetFiles(@"./", "*.jpg", System.IO.SearchOption.AllDirectories);
            var cascade_face = Accord.Vision.Detection.Cascades.FaceHaarCascade.FromXml(@"haarcascade_frontalface.xml");
            var face_detector = new Accord.Vision.Detection.HaarObjectDetector(cascade_face);

            foreach (var str in _files.Select((value, index) => new {value, index})) {
                Console.Write("Processing image {0} of {1}...", _files.Length, 1+@str.index);
                var image = new Bitmap(@str.value);
                var face = face_detector.ProcessFrame(image);
                Bitmap cropped = image.Clone(face[0], image.PixelFormat);
                cropped.Save(str.value.Substring(0, str.value.Length-4) + "_cropped.jpg");
                Console.Write("Done.\r\n");
            }
            Console.Write("Process Finished.");
        }
    }
}
