import os
import re
import requests
import json
from bs4 import BeautifulSoup
from requests.auth import HTTPBasicAuth


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

    url = 'http://localhost:8080/api/v1/jobdata'
    data = {
        "rustCount": rust_count,
        "pythonCount": python_count,
        "goCount": go_count
    }
    SPRING_SECURITY_USER = os.environ['SPRING_SECURITY_USER']
    SPRING_SECURITY_PASS = os.environ['SPRING_SECURITY_PASS']
    headers = {'Content-type': 'application/json'}
    response = requests.post(url, auth=HTTPBasicAuth(SPRING_SECURITY_USER, SPRING_SECURITY_PASS), data=json.dumps(data), headers=headers)


if __name__ == "__main__":
    main()
