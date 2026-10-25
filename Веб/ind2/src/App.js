import React, { useState } from 'react';
import content from './data/content.json';

function Heading({ id, children }) {
    return React.createElement(
        "h2",
        {
            id: `${id}-title`,
            className: "section-title text_border"
        },
        children
    );
}

function SocialLinks({ links }) {
    return React.createElement(
        "div",
        { className: "social-links" },
        links.map((link) =>
            React.createElement(
                "a",
                {
                    className: "social-link",
                    key: link.label,
                    href: link.url,
                    target: "_blank",
                    rel: "noopener noreferrer"
                },
                link.label
            )
        )
    );
}

function Header() {
    const [open, setOpen] = useState(false);

    return React.createElement(
        React.Fragment,
        null,

        React.createElement(
            "nav",
            {
                className: "menu",
                "aria-label": "Головна навігація"
            },

            React.createElement(
                "button",
                {
                    className: `menu_button ${open ? 'is-open' : ''}`,
                    type: "button",
                    "aria-expanded": open,
                    "aria-controls": "main-navigation",
                    "aria-label": open ? 'Закрити меню' : 'Відкрити меню',
                    onClick: () => setOpen((previous) => !previous)
                },
                React.createElement("span", null)
            ),

            React.createElement(
                "ul",
                {
                    id: "main-navigation",
                    className: `menu_list ${open ? 'is-open' : ''}`
                },
                content.menu.map((item, index) =>
                    React.createElement(
                        "li",
                        {
                            key: item.id,
                            className: index > 3 ? 'menu_extra' : ''
                        },

                        React.createElement(
                            "a",
                            {
                                className: "text_border",
                                href: `#${item.id}`,
                                onClick: () => setOpen(false)
                            },
                            item.title
                        )
                    )
                )
            )
        ),

        React.createElement(
            "header",
            { className: "header text_border" },
            React.createElement("h1", null, content.heroTitle)
        )
    );
}

function Feedback() {
    const [status, setStatus] = useState('');

    function submit(event) {
        event.preventDefault();
        setStatus(
            'Демонстраційна форма: дані перевірено, але не надіслано на сервер.'
        );
    }

    function reset() {
        setStatus('');
    }

    return React.createElement(
        "section",
        {
            id: "feedback",
            "aria-labelledby": "feedback-title"
        },

        React.createElement(
            Heading,
            { id: "feedback" },
            "Замовити консультацію"
        ),

        React.createElement(
            "p",
            { className: "section-intro" },
            content.feedback.intro
        ),

        React.createElement(
            "form",
            {
                className: "feedback-form",
                onSubmit: submit,
                onReset: reset
            },

            React.createElement(
                "div",
                { className: "form-field" },

                React.createElement(
                    "label",
                    { htmlFor: "name" },
                    "Ім’я та прізвище"
                ),

                React.createElement(
                    "input",
                    {
                        id: "name",
                        name: "name",
                        type: "text",
                        minLength: "2",
                        maxLength: "60",
                        placeholder: "Наприклад, Анна Коваль",
                        autoComplete: "name",
                        required: true
                    }
                )
            ),

            React.createElement(
                "div",
                { className: "form-field" },

                React.createElement(
                    "label",
                    { htmlFor: "email" },
                    "Електронна пошта"
                ),

                React.createElement(
                    "input",
                    {
                        id: "email",
                        name: "email",
                        type: "email",
                        placeholder: "name@example.com",
                        autoComplete: "email",
                        required: true
                    }
                )
            ),

            React.createElement(
                "div",
                { className: "form-field" },

                React.createElement(
                    "label",
                    { htmlFor: "phone" },
                    "Телефон"
                ),

                React.createElement(
                    "input",
                    {
                        id: "phone",
                        name: "phone",
                        type: "tel",
                        pattern: "[+0-9 ()-]{10,20}",
                        placeholder: "+380 67 123 45 67",
                        autoComplete: "tel",
                        required: true
                    }
                )
            ),

            React.createElement(
                "div",
                { className: "form-field" },

                React.createElement(
                    "label",
                    { htmlFor: "request-type" },
                    "Тема звернення"
                ),

                React.createElement(
                    "select",
                    {
                        id: "request-type",
                        name: "request-type",
                        defaultValue: "",
                        required: true
                    },

                    React.createElement(
                        "option",
                        {
                            value: "",
                            disabled: true
                        },
                        "Оберіть тему"
                    ),

                    content.feedback.topics.map((topic) =>
                        React.createElement(
                            "option",
                            {
                                value: topic.value,
                                key: topic.value
                            },
                            topic.label
                        )
                    )
                )
            ),

            React.createElement(
                "div",
                { className: "form-field form-field--full" },

                React.createElement(
                    "label",
                    { htmlFor: "message" },
                    "Ваше повідомлення"
                ),

                React.createElement(
                    "textarea",
                    {
                        id: "message",
                        name: "message",
                        minLength: "10",
                        maxLength: "500",
                        placeholder: "Напишіть, що саме вас цікавить...",
                        required: true
                    }
                )
            ),

            React.createElement(
                "div",
                { className: "form-consent" },

                React.createElement(
                    "input",
                    {
                        type: "checkbox",
                        id: "consent",
                        name: "consent",
                        required: true
                    }
                ),

                React.createElement(
                    "label",
                    { htmlFor: "consent" },
                    "Погоджуюся на обробку даних для отримання відповіді на звернення."
                )
            ),

            React.createElement(
                "div",
                { className: "form-actions" },

                React.createElement(
                    "button",
                    {
                        className: "form-button",
                        type: "submit"
                    },
                    "Надіслати заявку"
                ),

                React.createElement(
                    "button",
                    {
                        className: "form-button",
                        type: "reset"
                    },
                    "Очистити форму"
                )
            ),

            React.createElement(
                "p",
                {
                    role: "status",
                    "aria-live": "polite",
                    className: "form-note"
                },
                status || 'Усі поля форми потрібно заповнити перед надсиланням.'
            )
        )
    );
}

export default function App() {
    return React.createElement(
        "div",
        { className: "container" },

        React.createElement(Header, null),

        React.createElement(
            "main",
            { className: "main" },

            React.createElement(
                "section",
                {
                    id: "about",
                    "aria-labelledby": "about-title"
                },

                React.createElement(
                    Heading,
                    { id: "about" },
                    "Про теплицю"
                ),

                content.about.map((paragraph, index) =>
                    React.createElement(
                        "p",
                        { key: index },
                        paragraph
                    )
                )
            ),

            React.createElement(
                "section",
                {
                    id: "photo",
                    "aria-labelledby": "photo-title"
                },

                React.createElement(
                    Heading,
                    { id: "photo" },
                    "Наші рослини"
                ),

                React.createElement(
                    "div",
                    { className: "photoalbum" },

                    content.gallery.map((photo) =>
                        React.createElement(
                            "figure",
                            {
                                className: "photo_card",
                                key: photo.image
                            },

                            React.createElement(
                                "img",
                                {
                                    className: "photo_img",
                                    src: photo.image,
                                    alt: photo.alt
                                }
                            ),

                            React.createElement(
                                "figcaption",
                                {
                                    className: "photo_text text_border"
                                },
                                photo.caption
                            )
                        )
                    )
                )
            ),

            React.createElement(
                "section",
                {
                    id: "location",
                    "aria-labelledby": "location-title"
                },

                React.createElement(
                    Heading,
                    { id: "location" },
                    "Схема теплиці"
                ),

                React.createElement(
                    "figure",
                    { className: "map" },

                    React.createElement(
                        "img",
                        {
                            src: content.map.image,
                            alt: content.map.alt
                        }
                    )
                )
            ),

            React.createElement(
                "section",
                {
                    id: "social",
                    "aria-labelledby": "social-title"
                },

                React.createElement(
                    Heading,
                    { id: "social" },
                    "Наші переваги"
                ),

                content.social.map((paragraph, index) =>
                    React.createElement(
                        "p",
                        { key: index },
                        paragraph
                    )
                )
            ),

            React.createElement(Feedback, null),

            React.createElement(
                "section",
                {
                    id: "faq",
                    "aria-labelledby": "faq-title"
                },

                React.createElement(
                    Heading,
                    { id: "faq" },
                    "Часті запитання"
                ),

                React.createElement(
                    "div",
                    { className: "faq-list" },

                    content.faq.map((question) =>
                        React.createElement(
                            "details",
                            {
                                className: "faq-item",
                                key: question.question
                            },

                            React.createElement(
                                "summary",
                                null,
                                question.question
                            ),

                            React.createElement(
                                "div",
                                { className: "faq-answer" },

                                React.createElement(
                                    "p",
                                    null,
                                    question.answer
                                )
                            )
                        )
                    )
                )
            ),

            React.createElement(
                "section",
                {
                    id: "reviews",
                    "aria-labelledby": "reviews-title"
                },

                React.createElement(
                    Heading,
                    { id: "reviews" },
                    "Відгуки клієнтів"
                ),

                React.createElement(
                    "div",
                    { className: "reviews-grid" },

                    content.reviews.map((review) =>
                        React.createElement(
                            "article",
                            {
                                className: "review-card",
                                key: review.name
                            },

                            React.createElement(
                                "blockquote",
                                null,
                                review.quote
                            ),

                            React.createElement(
                                "figure",
                                { className: "review-author" },

                                React.createElement(
                                    "img",
                                    {
                                        className: "avatar",
                                        src: review.avatar,
                                        alt: `Аватар: ${review.name}`
                                    }
                                ),

                                React.createElement(
                                    "figcaption",
                                    null,

                                    React.createElement(
                                        "span",
                                        { className: "review-name" },
                                        review.name
                                    ),

                                    React.createElement(
                                        "span",
                                        {
                                            className: "review-rating",
                                            "aria-label": `Оцінка ${review.rating} з 5`
                                        },
                                        '★'.repeat(review.rating),
                                        '☆'.repeat(5 - review.rating)
                                    )
                                )
                            )
                        )
                    )
                )
            ),

            React.createElement(
                "section",
                {
                    id: "team",
                    "aria-labelledby": "team-title"
                },

                React.createElement(
                    Heading,
                    { id: "team" },
                    "Наша команда"
                ),

                React.createElement(
                    "div",
                    { className: "team-grid" },

                    content.team.map((member) =>
                        React.createElement(
                            "article",
                            {
                                className: "team-card",
                                key: member.name
                            },

                            React.createElement(
                                "img",
                                {
                                    className: "avatar",
                                    src: member.avatar,
                                    alt: `Аватар: ${member.name}`
                                }
                            ),

                            React.createElement(
                                "span",
                                { className: "team-badge" },
                                member.role
                            ),

                            React.createElement(
                                "h3",
                                null,
                                member.name
                            ),

                            React.createElement(
                                "p",
                                null,
                                member.description
                            ),

                            React.createElement(
                                SocialLinks,
                                { links: member.links }
                            )
                        )
                    )
                )
            ),

            React.createElement(
                "section",
                {
                    id: "history",
                    "aria-labelledby": "history-title"
                },

                React.createElement(
                    Heading,
                    { id: "history" },
                    "Історія теплиці"
                ),

                React.createElement(
                    "p",
                    { className: "section-intro" },
                    "Як розвивався «Зелений Дім» - від ідеї до першого врожаю."
                ),

                React.createElement(
                    "div",
                    { className: "timeline" },

                    content.history.map((item) =>
                        React.createElement(
                            "article",
                            {
                                className: "timeline-item",
                                key: item.year
                            },

                            React.createElement(
                                "time",
                                {
                                    className: "timeline-year",
                                    dateTime: item.year
                                },
                                item.year
                            ),

                            React.createElement(
                                "h3",
                                null,
                                item.title
                            ),

                            React.createElement(
                                "p",
                                null,
                                item.description
                            )
                        )
                    )
                )
            )
        ),

        React.createElement(
            "aside",
            { className: "ad" },

            React.createElement(
                "p",
                { className: "ad_text text_border" },
                content.adTitle
            )
        ),

        React.createElement(
            "footer",
            { className: "footer" },

            React.createElement(
                SocialLinks,
                { links: content.socialLinks }
            ),

            React.createElement(
                "p",
                null,
                content.footer
            )
        )
    );
}

