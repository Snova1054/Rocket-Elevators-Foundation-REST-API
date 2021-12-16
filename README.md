# Welcome to Rocket-Elevators-Foundation-REST-API
## First you'll have to either use Postman or you favorite API development environment
### For the newly added features, you can GET a list of all the Interventions according to a specific status that you'll input.
#### Simply add the endpoint after {/api/} without the curly brackets: interventions/{theStatusYouWant} and send it!

### Also, you can change the status a specific Intervention selected by the {id} and then by inputting the desired {status}.
#### You will simply have to add the endpoint after {api} without the curly brackets again: interventions/{theIDyouWant}/{theStatusYouWant}.
#### But there's more! If you change the {status} to either "InProgress" or "Completed" (case insensitive). You will change the following:
- if you input "InProgress", you will change the start_date of the Intervention to today's date.
- if you input "Completed", you will change the end_date of the Intervention to today's date.

### And lastly, if you ever want to verify any changes that you have made, you can simply GET the Intervention that you want to see by accessing the endpoint /api/interventions/{theIDyouWant}.

## That's it for the new things on the Website, Thank you!
