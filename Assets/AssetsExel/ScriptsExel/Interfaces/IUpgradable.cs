public interface IUpgradable{
    public int _maxLevel {get;set;}
    public int _level {get;set;}
    void Upgrade();
}