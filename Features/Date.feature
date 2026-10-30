@blackbox
@date
Feature: Date validation and leap year

  Scenario Outline: Basic date examples
    When I validate date <day>-<month>-<year>
    Then the date validity should be <expectedValid>
    And the leap year result should be <expectedLeapYear>

    Examples:
      | day | month | year | expectedValid | expectedLeapYear |
      | 29  | 2     | 2024 | true          | true             |
      | 29  | 2     | 2023 | false         | false            |
      | 31  | 4     | 2024 | false         | true             |
      | 15  | 8     | 2023 | true          | false            |

  @csv
  @DataSource:../Data/date.csv
  Scenario Outline: Validate dates from CSV
    When I validate date <day>-<month>-<year>
    Then the date validity should be <expectedValid>
    And the leap year result should be <expectedLeapYear>