// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
let currentIndex = 0;
const images = ["images/announcment.jpg", "images/announcment2.jpg", "images/announcment1.jpg" ,"images/announcment3.jpg"]; 

function changeImage(indexChange) {
    currentIndex += indexChange;

    if (currentIndex < 0) {
        currentIndex = images.length - 1;
    } else if (currentIndex >= images.length) {
        currentIndex = 0;
    }

    const sliderImage = document.getElementById("slider-image");
    sliderImage.src = images[currentIndex];
}
document.getElementById("description").addEventListener("input", function () {
    var wordCount = this.value.trim().split(/\s+/).length;
    var wordCountMessage = document.getElementById("wordCountMessage");

    if (wordCount < 20) {
        wordCountMessage.textContent = "Description must contain at least 20 words.";
        this.setCustomValidity("Description must contain at least 20 words.");
    } else {
        wordCountMessage.textContent = "";
        this.setCustomValidity("");
    }
});
function toggleAdvancedFilters() {
    var advancedFilters = document.getElementById('advanced-filters');
    var toggleButton = document.getElementById('toggle-advanced-filters');

    if (advancedFilters.style.display === 'none' || advancedFilters.style.display === '') {
        advancedFilters.style.display = 'block';
        toggleButton.textContent = 'Show Less Filters';
    } else {
        advancedFilters.style.display = 'none';
        toggleButton.textContent = 'Show More Filters';
    }
}
document.addEventListener('DOMContentLoaded', () => {
    const openReplies = '@(ViewData["OpenReplies"] ?? "")'.split(',');
    openReplies.forEach(commentId => {
        const repliesDiv = document.getElementById(`replies-${commentId}`);
        if (repliesDiv) {
            repliesDiv.style.display = 'block';
        }
    });
});

function toggleReplies(commentId) {
    const repliesDiv = document.getElementById(`replies-${commentId}`);
    if (repliesDiv.style.display === 'block') {
        repliesDiv.style.display = 'none';
    } else {
        repliesDiv.style.display = 'block';
    }
    updateOpenReplies();
}

function updateOpenReplies() {
    const openReplies = [];
    document.querySelectorAll('.replies').forEach(div => {
        if (div.style.display === 'block') {
            openReplies.push(div.id.split('-')[1]);
        }
    });
    document.querySelectorAll('input[name="openReplies"]').forEach(input => {
        input.value = openReplies.join(',');
    });
}
function toggleElement(elementId) {
    const element = document.getElementById(elementId);
    const isHidden = element.style.display === 'none' || element.style.display === '';

    // Hide all forms except the add blog form
    document.querySelectorAll('.editForm, .deleteForm').forEach(form => {
        if (form.id !== 'addBlogForm') {
            form.style.display = 'none';
        }
    });

    // Show the selected form if it was hidden
    if (isHidden) {
        element.style.display = 'block';
    }
}

function toggleBlogEdit(blogId) {
    const editForm = document.getElementById(`editBlogForm-${blogId}`);
    const deleteForm = document.getElementById(`deleteBlogForm-${blogId}`);
    if (editForm.style.display === 'none' || editForm.style.display === '') {
        editForm.style.display = 'block';
        deleteForm.style.display = 'none';
    } else {
        editForm.style.display = 'none';
    }
}

function toggleBlogDelete(blogId) {
    const deleteForm = document.getElementById(`deleteBlogForm-${blogId}`);
    const editForm = document.getElementById(`editBlogForm-${blogId}`);
    if (deleteForm.style.display === 'none' || deleteForm.style.display === '') {
        deleteForm.style.display = 'block';
        editForm.style.display = 'none';
    } else {
        deleteForm.style.display = 'none';
    }
}

function toggleAddBlogForm() {
    const addBlogForm = document.getElementById('addBlogForm');
    const addBlogButton = document.getElementById('addBlogButton');
    if (addBlogForm.style.display === 'none' || addBlogForm.style.display === '') {
        addBlogForm.style.display = 'block';
        addBlogButton.classList.add('active');
    } else {
        addBlogForm.style.display = 'none';
        addBlogButton.classList.remove('active');
    }
}

function toggleCommentEdit(commentId) {
    const editForm = document.getElementById(`editCommentForm-${commentId}`);
    const deleteForm = document.getElementById(`deleteCommentForm-${commentId}`);
    if (editForm.style.display === 'none' || editForm.style.display === '') {
        editForm.style.display = 'block';
        deleteForm.style.display = 'none';
    } else {
        editForm.style.display = 'none';
    }
}

function toggleCommentDelete(commentId) {
    const deleteForm = document.getElementById(`deleteCommentForm-${commentId}`);
    const editForm = document.getElementById(`editCommentForm-${commentId}`);
    if (deleteForm.style.display === 'none' || deleteForm.style.display === '') {
        deleteForm.style.display = 'block';
        editForm.style.display = 'none';
    } else {
        deleteForm.style.display = 'none';
    }
}
function showCommentBox() {
    const commentBox = document.getElementById("commentBox");
    if (commentBox.style.display === "none") {
        commentBox.style.display = "block";
    } else {
        commentBox.style.display = "none";
    }
}
function searchComments() {
    const searchText = document.getElementById("searchComments").value.toLowerCase();
    const comments = document.querySelectorAll(".comment");
    comments.forEach(comment => {
        const commentText = comment.querySelector(".comment-text").textContent.toLowerCase();
        if (commentText.includes(searchText)) {
            comment.style.display = "block";
        } else {
            comment.style.display = "none";
        }
    });
}
function sortComments() {
    const sortValue = document.getElementById("sortComments").value;
    const url = new URL(window.location.href);
    url.searchParams.set("SortOrder", sortValue);
    window.location.href = url.toString();
}