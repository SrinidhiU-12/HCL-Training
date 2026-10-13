

const searchInput = document.getElementById("courseSearch");

if (searchInput) {

    searchInput.addEventListener("input", function () {

        const searchValue = this.value.toLowerCase().trim();

        const courses = document.querySelectorAll(".course-item");

        let visibleCourses = 0;

        courses.forEach(function (course) {

            const courseName =
                course.getAttribute("data-name").toLowerCase();

            if (courseName.includes(searchValue)) {

                course.style.display = "block";
                visibleCourses++;

            } else {

                course.style.display = "none";

            }

        });


        const noCourses =
            document.getElementById("noCourses");

        if (noCourses) {

            if (visibleCourses === 0) {

                noCourses.classList.remove("d-none");

            } else {

                noCourses.classList.add("d-none");

            }

        }

    });

}




const filterButtons =
    document.querySelectorAll(".filter-btn");

if (filterButtons.length > 0) {

    filterButtons.forEach(function (button) {

        button.addEventListener("click", function () {

            const filter =
                this.getAttribute("data-filter");

            filterButtons.forEach(function (btn) {

                btn.classList.remove("btn-primary");

                btn.classList.add("btn-outline-primary");

            });


            this.classList.remove("btn-outline-primary");

            this.classList.add("btn-primary");


            const courses =
                document.querySelectorAll(".course-item");

            let visibleCourses = 0;

            courses.forEach(function (course) {

                const category =
                    course.getAttribute("data-category");

                if (
                    filter === "all" ||
                    category === filter
                ) {

                    course.style.display = "block";

                    visibleCourses++;

                } else {

                    course.style.display = "none";

                }

            });


            const noCourses =
                document.getElementById("noCourses");

            if (noCourses) {

                if (visibleCourses === 0) {

                    noCourses.classList.remove("d-none");

                } else {

                    noCourses.classList.add("d-none");

                }

            }

        });

    });

}




const contactForm =
    document.getElementById("contactForm");

if (contactForm) {

    contactForm.addEventListener("submit", function (event) {

        event.preventDefault();

        alert(
            "Thank you! Your message has been submitted successfully."
        );

        contactForm.reset();

    });

}




const loginForm =
    document.getElementById("loginForm");

if (loginForm) {

    loginForm.addEventListener("submit", function (event) {

        event.preventDefault();

        alert(
            "Login successful! Welcome to Learnify."
        );

        loginForm.reset();

    });

}


const newsletter =
    document.querySelector(".newsletter-form");

if (newsletter) {

    newsletter.addEventListener("submit", function (event) {

        event.preventDefault();

        alert(
            "Thank you for subscribing to Learnify!"
        );

        newsletter.reset();

    });

}


const cards =
    document.querySelectorAll(
        ".course-card, .category-card, .instructor-card"
    );

const observer =
    new IntersectionObserver(
        function (entries) {

            entries.forEach(function (entry) {

                if (entry.isIntersecting) {

                    entry.target.style.opacity = "1";
                    entry.target.style.transform = "translateY(0)";

                }

            });

        },
        {
            threshold: 0.1
        }
    );


cards.forEach(function (card) {

    card.style.opacity = "0";
    card.style.transform = "translateY(20px)";
    card.style.transition = "0.6s ease";

    observer.observe(card);

});