@blackbox
@password
Feature: Password validation

  Scenario Outline: Basic examples (inline)
    When I validate the password "<password>"
    Then the password result should be "<expected>"

    Examples:
      | password  | expected                 |
      | Abcdefg1  | Valid                    |
      | Abcdef1   | TooShort                 |
      | abcdefg1  | NoUpper                  |
      | Abcdefgh  | NoDigit                  |
      | abc       | TooShort;NoUpper;NoDigit |

  @csv
  @DataSource:../Data/password.csv
  Scenario Outline: Password cases from CSV
    When I validate the password "<password>"
    Then the password result should be "<expected>"

  Scenario: An empty password violates all rules
    When I validate an empty password
    Then the password result should be "TooShort;NoUpper;NoDigit"
