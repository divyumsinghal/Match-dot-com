# MatchDotCom.UserProfile Test Suite

This test project provides comprehensive coverage for the MatchDotCom.UserProfile class library.

## Test Coverage

### Core Classes

- **UserProfile** - 15 tests covering constructor validation, property setting, JSON serialization
- **Contact** - 8 tests covering constructor, properties, JSON serialization, audit fields
- **Address** - 11 tests covering constructor, geocoding, JSON serialization, coordinate validation
- **ProfileBio** - 15 tests covering constructor, multiple interests/gender preferences, JSON serialization

### Supporting Classes

- **Coordinates** - 11 tests covering latitude/longitude validation, precision, equality
- **MatchCriteria** - 11 tests covering age range, distance, interests filtering

### Enums

- **GenderOptions** - 7 tests covering enum values, conversion, collections usage
- **Interests** - 10 tests covering all interest types, categorization, collections

### Services

- **Geocoder** - 14 tests covering geocoding service, fallback handling, rate limiting

## Test Categories

### Validation Tests

- Username validation (length, characters)
- Name validation (first, middle, last)
- Age validation (minimum age requirements)
- Email and phone number format validation
- Address field validation

### Business Logic Tests

- Profile creation with valid data
- Date of birth calculations
- Geographic coordinate handling
- Interest and gender preference handling

### Integration Tests

- JSON serialization/deserialization
- Geocoding service integration
- Database field mapping

### Error Handling Tests

- Invalid input handling
- Null reference handling
- Service failure scenarios

## Test Frameworks Used

- **xUnit** - Primary testing framework
- **FluentAssertions** - Assertion library for readable tests
- **Moq** - Mocking framework for dependencies

## Running Tests

```bash
dotnet test MatchDotCom.UserProfile.Tests/
```

## Test Statistics

- **Total Tests**: 152
- **Test Classes**: 9
- **Coverage Areas**: All public APIs and business logic
- **Test Types**: Unit tests, integration tests, validation tests

## Notes

- Some geocoding tests make real API calls to OpenStreetMap Nominatim service
- Rate limiting is respected in geocoding tests (1 second delay)
- Tests cover both happy path and error scenarios
- All required and optional parameters are tested
- Edge cases and boundary conditions are covered
