
using Birds;

public class BirdObjectPool: ObjectPool
{
    private BirdData _birdData;

    public void Initialize(BirdData birdData, int initialSize)
    {
        _birdData = birdData;
        base.Initialize(null, initialSize);
    }
    
    protected override PoolableObject Instantiate()
    {
        return BirdFactory.Instance.CreateBird(_birdData).GetComponent<PoolableObject>();
    }
}
