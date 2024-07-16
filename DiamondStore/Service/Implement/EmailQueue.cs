namespace Service.Services.Impl
{
    public class EmailQueue : IEmailQueue
    {
        private readonly Queue<string> _queue = new Queue<string>();

        public async Task EnqueueEmailAsync(string recipientEmail)
        {
            _queue.Enqueue(recipientEmail);
            await Task.CompletedTask; // Simulate async operation
        }

        public async Task<string?> DequeueEmailAsync()
        {
            if (_queue.Count > 0)
            {
                return _queue.Dequeue();
            }
            return await Task.FromResult<string?>(null); // Simulate async operation
        }

        public Task<IEnumerable<string>> GetQueue()
        {
            return Task.FromResult<IEnumerable<string>>(_queue.ToList());
        }
    }
}
