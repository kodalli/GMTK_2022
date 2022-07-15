public interface IInputProvider {
    public InputState GetState();
    void EnableInput();
    void DisableInput();
}