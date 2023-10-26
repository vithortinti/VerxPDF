namespace VerxPDF.Domain.Interfaces.Repositories
{
    public interface IData<TIdentifier, TModel>
    {
        /// <summary>
        /// Get the model by id.
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Model by ID</returns>
        TModel? Get(TIdentifier id);

        /// <summary>
        /// Get all the models available.
        /// </summary>
        /// <returns></returns>
        List<TModel>? GetAll();

        /// <summary>
        /// Update a model.
        /// </summary>
        /// <param name="model">Model to update</param>
        /// <returns></returns>
        bool Update(TModel model);

        /// <summary>
        /// Creates a new model based on the template created.
        /// </summary>
        /// <param name="model">Model to create.</param>
        /// <returns></returns>
        void Create(TModel model);

        /// <summary>
        /// Removes a model.
        /// </summary>
        /// <param name="model">Model to delete</param>
        bool Delete(TModel model);

        /// <summary>
        /// Saves the model.
        /// </summary>
        void Save();
    }
}