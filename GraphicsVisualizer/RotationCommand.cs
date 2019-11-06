namespace GraphicsVisualizer
{
    class RotationCommand : ITransformationCommand
    {
        public GraphicsComponent Act(GraphicsComponent component)
        {
            return component;
        }
    }
}
