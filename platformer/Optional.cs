namespace platformer
{
    class Optional<T>
    {
        bool _HasValue;
        public bool HasValue {get => _HasValue;}

        T _Value;
        public T Value 
        {
            set 
            {
                _HasValue = true; _Value = value;
            } 
            get 
            {
                if (HasValue) { return _Value; }
                else { throw new System.Exception("OPTIONAL ERROR: Attempting to read the value of an Optional<T> with no value"); }
            }
        }

        public Optional()
        {
            _HasValue = false;
            Value = default(T);
        }

        public Optional(T value)
        {
            _HasValue = true;
            this.Value = value;
        }

        public void Clear()
        {
            _HasValue = false;
            Value = default(T);
        }
    }
}