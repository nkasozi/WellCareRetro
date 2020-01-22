# WellCareMiddleware

This Repo holds the middleware code for the WellCareMiddleware Site. It's composed of a .net core Web API and Actors based on Akka.net. Subprojects have been created in order to create a cleaner design and make it easier for newcomers to understand the design and to reduce their time to first meaningful contributions

## Installation

Use git to download this repo and begin contributing

```bash
git clone 
https://WellCareAppRetro@dev.azure.com/WellCareAppRetro/WellCareAppMiddleware/_git/WellCareAppMiddleware
```

## Demo
Here is a working live demo: 

```bash
https://testnv.azurewebsites.net/api/newsposts/1
```
### Development
Want to contribute? Great!

To fix a bug or enhance an existing module, follow these steps:

- clone the repo
- Create a new branch (`git checkout -b improve-feature`)
- Make the appropriate changes in the files
- Add changes to reflect the changes made
- Add unit tests to test the changes made
- Add integration tests to test the feature/behavior
- Commit your changes (`git commit -am 'Improve feature'`)
- Push to the branch (`git push origin improve-feature`)
- Create a Pull Request 

## Notes on the Code organization.

The Backend Code relies heavily on Akka.net Actors. Akka is an opinionated framework that forces the developer to put any behavior or action-oriented code behind actors and the only way to invoke said actions is through the use of lightweight CQRs classes. This simplifies code organization and makes it easier to standardize and maintain code.

Simply put
- Actors go in core.Actors
- Entities go in core.Entities 
- etc

A note on tests...there are 2 kinds of test projects in the repo. 

- Unit tests deal with testing the logic rules of a single actor. that means any dependencies of that actor are mocked out. 
- Integration tests test the behavior of an actor given actual real-world dependencies.

Don't mix the 2 up by putting unit tests in the integration tests project or vice versa

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update the tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)