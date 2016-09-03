namespace dougnlamb.budget.models {
    public interface IAllocationViewModel {
        int oid { get; }

        IMoneyViewModel Amount { get; }
        string Notes { get; }
    }
}