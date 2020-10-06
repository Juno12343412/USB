namespace Pooling
{
    // 1 단계 풀링할 오브젝트가 기본적으로 가지고 있어야할 요소
    public interface IPoolingInit 
    {
        // # Base Object Data #
        string objectName { get; set; }
        bool isActive { get; set; }

        void Init();
        void Release();
    }
}
