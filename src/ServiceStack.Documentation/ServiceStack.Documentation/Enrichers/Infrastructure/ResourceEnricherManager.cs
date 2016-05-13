namespace ServiceStack.Documentation.Enrichers.Infrastructure
{
    using System;
    using Extensions;
    using Host;
    using Interfaces;
    using Models;

    /// <summary>
    /// Manages default logic for enriching resources
    /// </summary>
    public class ResourceEnricherManager
    {
        private readonly IResourceEnricher resourceEnricher;
        private readonly PropertyEnricherManager _propertyEnricherManager;

        public ResourceEnricherManager(IResourceEnricher resourceEnricher, IPropertyEnricher propertyEnricher)
        {
            this.resourceEnricher = resourceEnricher;
            _propertyEnricherManager = new PropertyEnricherManager(propertyEnricher, EnrichResource);
        }

        /// <summary>
        /// Enrich supplied IApiResourceType object with details from Operation object
        /// </summary>
        /// <param name="resource">The object to be enriched</param>
        /// <param name="operation">Details of operation to use for enrichment</param>
        public void EnrichResource(IApiResourceType resource, Operation operation)
        {
            // The object that has ResponseStatus is built up from request object
            var type = resource is IApiResponseStatus ? operation.RequestType : operation.ResponseType;
            EnrichResource(resource, type);
        }

        private void EnrichResource(IApiResourceType resource, Type type)
        {
            // The object that has ResponseStatus is built up from request object
            if (resourceEnricher != null)
            {
                resource.Title = resource.Title.GetIfNullOrEmpty(() => resourceEnricher.GetTitle(type));
                resource.Description = resource.Description.GetIfNullOrEmpty(() => resourceEnricher.GetDescription(type));
                resource.Notes = resource.Notes.GetIfNullOrEmpty(() => resourceEnricher.GetNotes(type));
            }

            resource.Properties = _propertyEnricherManager.EnrichParameters(resource.Properties, type);
        }
    }
}