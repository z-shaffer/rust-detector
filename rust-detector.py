import re
import requests
import json
from bs4 import BeautifulSoup


def find_target_pattern(tag, target_pattern):
    return tag.name and re.search(target_pattern, tag.get_text())


def scraper(target_pattern, language):
    url = f"https://www.devjobsscanner.com/{language}-jobs/"
    response = requests.get(url)
    if response.status_code == 200:
        soup = BeautifulSoup(response.text, 'html.parser')
        occurrences = soup.find(lambda tag: find_target_pattern(tag, target_pattern))
        for occurrence in occurrences:
            match = re.search(r'\d+', occurrence.get_text())
            if match:
                number = match.group()
                print(f"Found: {number} {language} jobs")
                return number
    else:
        print(f"Failed to retrieve the page. Status code: {response.status_code}")


def main():
    target_pattern = r'\d+ open positions'
    rust_count = scraper(target_pattern, "rust")
    python_count = scraper(target_pattern, "python")
    go_count = scraper(target_pattern, "go")
    # cpp_count = scraper(target_pattern, "c++")

    url = 'https://rustdetector.azurewebsites.net/jobdata'
    data = {
        "rustCount": rust_count,
        "pythonCount": python_count,
        "goCount": go_count
    }
    headers = {'Content-type': 'application/json'}
    response = requests.post(url, data=json.dumps(data), headers=headers)
    print(response)


if __name__ == "__main__":
    main()
