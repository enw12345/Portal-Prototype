namespace InteractBehaviour
{
    public class GrabBehaviour : InteractBehaviour
    {
        protected override void Interact()
        {
            Grab();
        }

        private void Grab()
        {
            if (interacting)
            {
                InteractingObject.Interact();
                interacting = false;
            }
            else
            {
                InteractingObject.Interact();
                interacting = true;
            }
        }
    }
}