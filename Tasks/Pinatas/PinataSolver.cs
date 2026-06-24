namespace Pinatas
{
    internal class PinataSolver
    {
        private int[] _extendedPinatas;
        private int[,] _memo;
        public int CalculateMaxAmount(int[] pinatas)
        {
            int n = pinatas.Length;

            _extendedPinatas = new int[n + 2];
            _memo = new int[n + 2, n + 2];

            _extendedPinatas[0] = 1;
            _extendedPinatas[n + 1] = 1;

            for (int i = 0; i < n; i++)
            {
                _extendedPinatas[i + 1] = pinatas[i];
            }

            return GetMaxAmount(0, n + 1);
        }

        private int GetMaxAmount(int left, int right)
        {
            if (left + 1 == right)
                return 0;
            if (_memo[left, right] > 0)
                return _memo[left, right];

            int maxAmount = 0;

            for (int i = left + 1; i < right; i++)
            {
                int currentCandidate = _extendedPinatas[left] * _extendedPinatas[i] * _extendedPinatas[right];

                int leftPart = GetMaxAmount(left, i);
                int rightPart = GetMaxAmount(i, right);

                int total = currentCandidate + leftPart + rightPart;

                if (total > maxAmount)
                    maxAmount = total;
            }

            _memo[left, right] = maxAmount;
            return maxAmount;
        }
    }
}
