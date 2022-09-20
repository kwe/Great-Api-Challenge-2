package main

import (
	"encoding/json"
	"io"
	"net/http"
	"time"

	"github.com/gofiber/fiber/v2"
)

type Posts []Post

func UnmarshalPosts(data []byte) (Posts, error) {
	var r Posts
	err := json.Unmarshal(data, &r)
	return r, err
}

func (r *Posts) Marshal() ([]byte, error) {
	return json.Marshal(r)
}

type Post struct {
	UserID    int64  `json:"userId"`
	ID        int64  `json:"id"`
	Title     string `json:"title"`
	Completed bool   `json:"completed"`
}

func main() {
	app := fiber.New()

	app.Get("/", func(c *fiber.Ctx) error {
		return c.JSON(fiber.Map{
			"message": "Hello, World!",
		})
	})

	// return a joke as an json object
	app.Get("/joke", func(c *fiber.Ctx) error {
		return c.JSON(fiber.Map{
			"joke": "What do you call a fake noodle? An impasta.",
		})
	})

	// return the current time as a json object
	app.Get("/time", func(c *fiber.Ctx) error {
		return c.JSON(fiber.Map{
			"time": time.Now(),
		})
	})

	// return a list of posts as a json object
	app.Get("/posts", func(c *fiber.Ctx) error {

		// get the posts
		posts, err := getPosts()
		if err != nil {
			return c.Status(http.StatusInternalServerError).JSON(fiber.Map{
				"error": err.Error(),
			})
		}

		// return the posts
		return c.JSON(posts)
	})

	app.Listen(":3000")
}

func getPosts() (Posts, error) {
	resp, err := http.Get("https://jsonplaceholder.typicode.com/posts")
	if err != nil {
		return nil, err
	}
	defer resp.Body.Close()

	body, err := io.ReadAll(resp.Body)
	if err != nil {
		return nil, err
	}

	return UnmarshalPosts(body)
}
