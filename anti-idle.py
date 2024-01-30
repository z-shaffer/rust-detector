import requests

def perform_get_request(url):
    try:
        # Perform a GET request to the specified URL
        response = requests.get(url)

        # Check if the request was successful (status code 200)
        if response.status_code == 200:
            print("GET request successful!")
            print(response.text)
        else:
            print(f"GET request failed with status code: {response.status_code}")

    except requests.RequestException as e:
        print(f"Error during GET request: {e}")

perform_get_request('https://rustdetector.azurewebsites.net/jobdata')

