﻿using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using ShellProgressBar;
using SLS.Marketing.ResponseBuilding;
using SLS.Porfolio.Repository;
using SLS.Porfolio.Repository.Entiies;

string? cosmosConnectionString = Environment.GetEnvironmentVariable("ASW_CosmosConnectionString");

if (cosmosConnectionString is not null)
{

	using PortfolioContext portfolioContext = new();

	using CosmosClient cosmosClient = new(cosmosConnectionString);
	Database cosmosDatabase = await cosmosClient.CreateDatabaseIfNotExistsAsync("ASW");
	Container container = await cosmosDatabase.CreateContainerIfNotExistsAsync("Northstar", "/Discriminator");

	IGetCommunityDetails getCommunityDetails = new GetCommunityDetails(portfolioContext, container);

	List<Community>? communities = await portfolioContext.Communities.ToListAsync();
	using ProgressBar progressBar = new(communities.Count, $"Building responses (1 of {communities.Count})");
	foreach (Community community in communities)
	{
		await getCommunityDetails.BuildAsync(community.CommunityNumber);
		progressBar.Tick($"Building responses ({progressBar.CurrentTick + 1} of {progressBar.MaxTicks})");
	}

}