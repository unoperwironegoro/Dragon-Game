public interface IFlapController {
    bool Flap(ControlDir dir);
    bool Release(ControlDir dir);
}

public enum ControlDir {
    LEFT,
    RIGHT
}