namespace GocTenderNotices.Contracts.State
{
    public enum ProcurementStatus
    {
        Active,
        Expired,
        Awarded,
        /// <summary>
        /// When updating, does not add, only updates if present.
        /// </summary>
        Amended
    }
}