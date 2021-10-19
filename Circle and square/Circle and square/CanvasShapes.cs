using System.Collections.Generic;
using System.Windows.Shapes;

namespace Circle_and_square
{
    
    class CanvasShapes
    {
        /*
         * Stores list of Shape objects present on canvas and allows to manipulate that list.
         */

        private readonly List<Shape> shapes;

        public CanvasShapes()
        {
            shapes = new List<Shape>();
        }
        public void AddShape(Ellipse shape)
        {
           shapes.Add(shape);
        }
        public void AddShape(Rectangle shape)
        {
            shapes.Add(shape);
        }
        public int HowManyCircles()
        {
            int i = 0;
            foreach (Shape shape in shapes)
            {
                if (shape is Ellipse)
                {
                    i++;
                }
            }
            return i;
        }
        public int HowManySquares()
        {
            int i = 0;
            foreach (Shape shape in shapes)
            {
                if (shape is Rectangle)
                {
                    i++;
                }
            }
            return i;
        }
        public void RemoveShape(Shape shapeToRemove)
        {
            shapes.Remove(shapeToRemove);
            RenameShapes();
        }
        public int HowManyShapes()
        {
            return shapes.Count;
        }
        private void RenameShapes()
        {
            int i = 0;
            int j = 0;

            for (int k = 0; k < shapes.Count; k++)
            {
                if (shapes[k] is Ellipse)
                {
                    shapes[k].Name = "Circle" + i;
                    i++;
                }
                else
                {
                    shapes[k].Name = "Square" + j;
                    j++;
                }
            }
        }
        public Shape GetShape(int shapeToGet)
        {
            return shapes[shapeToGet];
        }
    }
}
