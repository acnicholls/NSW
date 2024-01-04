namespace NSW.GeoCoder.Interfaces
{
	public interface IDatabase
	{
		void ClearDatabaseFkConstraint();
		void AddNewPostalCodes();
		void ModifyTblUsersPostalCodes();
		void ReAddFKConstraintOnTblUsers();
	}
}
