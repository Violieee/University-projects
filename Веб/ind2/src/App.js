import React, { useState } from 'react';
import content from './data/content.json';
function Heading({ id, children }) {
    return (React.createElement("h2", { id: `${id}-title`, className: "section-title text_border" }, children));
}
function SocialLinks({ links }) {
    return (React.createElement("div", { className: "social-links" }, links.map((link) => (React.createElement("a", { className: "social-link", key: link.label, href: link.url, target: "_blank", rel: "noopener noreferrer" }, link.label)))));
}
function Header() {
    const [open, setOpen] = useState(false);
    return (React.createElement(React.Fragment, null,
        React.createElement("nav", { className: "menu", "aria-label": "\u0413\u043E\u043B\u043E\u0432\u043D\u0430 \u043D\u0430\u0432\u0456\u0433\u0430\u0446\u0456\u044F" },
            React.createElement("button", { className: `menu_button ${open ? 'is-open' : ''}`, type: "button", "aria-expanded": open, "aria-controls": "main-navigation", "aria-label": open ? 'Закрити меню' : 'Відкрити меню', onClick: () => setOpen((previous) => !previous) },
                React.createElement("span", null)),
            React.createElement("ul", { id: "main-navigation", className: `menu_list ${open ? 'is-open' : ''}` }, content.menu.map((item, index) => (React.createElement("li", { key: item.id, className: index > 3 ? 'menu_extra' : '' },
                React.createElement("a", { className: "text_border", href: `#${item.id}`, onClick: () => setOpen(false) }, item.title)))))),
        React.createElement("header", { className: "header text_border" },
            React.createElement("h1", null, content.heroTitle))));
}
function Feedback() {
    const [status, setStatus] = useState('');
    function submit(event) {
        event.preventDefault();
        setStatus('Демонстраційна форма: дані перевірено, але не надіслано на сервер.');
    }
    function reset() {
        setStatus('');
    }
    return (React.createElement("section", { id: "feedback", "aria-labelledby": "feedback-title" },
        React.createElement(Heading, { id: "feedback" }, "\u0417\u0430\u043C\u043E\u0432\u0438\u0442\u0438 \u043A\u043E\u043D\u0441\u0443\u043B\u044C\u0442\u0430\u0446\u0456\u044E"),
        React.createElement("p", { className: "section-intro" }, content.feedback.intro),
        React.createElement("form", { className: "feedback-form", onSubmit: submit, onReset: reset },
            React.createElement("div", { className: "form-field" },
                React.createElement("label", { htmlFor: "name" }, "\u0406\u043C\u2019\u044F \u0442\u0430 \u043F\u0440\u0456\u0437\u0432\u0438\u0449\u0435"),
                React.createElement("input", { id: "name", name: "name", type: "text", minLength: "2", maxLength: "60", placeholder: "\u041D\u0430\u043F\u0440\u0438\u043A\u043B\u0430\u0434, \u0410\u043D\u043D\u0430 \u041A\u043E\u0432\u0430\u043B\u044C", autoComplete: "name", required: true })),
            React.createElement("div", { className: "form-field" },
                React.createElement("label", { htmlFor: "email" }, "\u0415\u043B\u0435\u043A\u0442\u0440\u043E\u043D\u043D\u0430 \u043F\u043E\u0448\u0442\u0430"),
                React.createElement("input", { id: "email", name: "email", type: "email", placeholder: "name@example.com", autoComplete: "email", required: true })),
            React.createElement("div", { className: "form-field" },
                React.createElement("label", { htmlFor: "phone" }, "\u0422\u0435\u043B\u0435\u0444\u043E\u043D"),
                React.createElement("input", { id: "phone", name: "phone", type: "tel", pattern: "[+0-9 ()-]{10,20}", placeholder: "+380 67 123 45 67", autoComplete: "tel", required: true })),
            React.createElement("div", { className: "form-field" },
                React.createElement("label", { htmlFor: "request-type" }, "\u0422\u0435\u043C\u0430 \u0437\u0432\u0435\u0440\u043D\u0435\u043D\u043D\u044F"),
                React.createElement("select", { id: "request-type", name: "request-type", defaultValue: "", required: true },
                    React.createElement("option", { value: "", disabled: true }, "\u041E\u0431\u0435\u0440\u0456\u0442\u044C \u0442\u0435\u043C\u0443"),
                    content.feedback.topics.map((topic) => (React.createElement("option", { value: topic.value, key: topic.value }, topic.label))))),
            React.createElement("div", { className: "form-field form-field--full" },
                React.createElement("label", { htmlFor: "message" }, "\u0412\u0430\u0448\u0435 \u043F\u043E\u0432\u0456\u0434\u043E\u043C\u043B\u0435\u043D\u043D\u044F"),
                React.createElement("textarea", { id: "message", name: "message", minLength: "10", maxLength: "500", placeholder: "\u041D\u0430\u043F\u0438\u0448\u0456\u0442\u044C, \u0449\u043E \u0441\u0430\u043C\u0435 \u0432\u0430\u0441 \u0446\u0456\u043A\u0430\u0432\u0438\u0442\u044C...", required: true })),
            React.createElement("div", { className: "form-consent" },
                React.createElement("input", { type: "checkbox", id: "consent", name: "consent", required: true }),
                React.createElement("label", { htmlFor: "consent" }, "\u041F\u043E\u0433\u043E\u0434\u0436\u0443\u044E\u0441\u044F \u043D\u0430 \u043E\u0431\u0440\u043E\u0431\u043A\u0443 \u0434\u0430\u043D\u0438\u0445 \u0434\u043B\u044F \u043E\u0442\u0440\u0438\u043C\u0430\u043D\u043D\u044F \u0432\u0456\u0434\u043F\u043E\u0432\u0456\u0434\u0456 \u043D\u0430 \u0437\u0432\u0435\u0440\u043D\u0435\u043D\u043D\u044F.")),
            React.createElement("div", { className: "form-actions" },
                React.createElement("button", { className: "form-button", type: "submit" }, "\u041D\u0430\u0434\u0456\u0441\u043B\u0430\u0442\u0438 \u0437\u0430\u044F\u0432\u043A\u0443"),
                React.createElement("button", { className: "form-button", type: "reset" }, "\u041E\u0447\u0438\u0441\u0442\u0438\u0442\u0438 \u0444\u043E\u0440\u043C\u0443")),
            React.createElement("p", { role: "status", "aria-live": "polite", className: "form-note" }, status || 'Усі поля форми потрібно заповнити перед надсиланням.'))));
}
export default function App() {
    return (React.createElement("div", { className: "container" },
        React.createElement(Header, null),
        React.createElement("main", { className: "main" },
            React.createElement("section", { id: "about", "aria-labelledby": "about-title" },
                React.createElement(Heading, { id: "about" }, "\u041F\u0440\u043E \u0442\u0435\u043F\u043B\u0438\u0446\u044E"),
                content.about.map((paragraph, index) => (React.createElement("p", { key: index }, paragraph)))),
            React.createElement("section", { id: "photo", "aria-labelledby": "photo-title" },
                React.createElement(Heading, { id: "photo" }, "\u041D\u0430\u0448\u0456 \u0440\u043E\u0441\u043B\u0438\u043D\u0438"),
                React.createElement("div", { className: "photoalbum" }, content.gallery.map((photo) => (React.createElement("figure", { className: "photo_card", key: photo.image },
                    React.createElement("img", { className: "photo_img", src: photo.image, alt: photo.alt }),
                    React.createElement("figcaption", { className: "photo_text text_border" }, photo.caption)))))),
            React.createElement("section", { id: "location", "aria-labelledby": "location-title" },
                React.createElement(Heading, { id: "location" }, "\u0421\u0445\u0435\u043C\u0430 \u0442\u0435\u043F\u043B\u0438\u0446\u0456"),
                React.createElement("figure", { className: "map" },
                    React.createElement("img", { src: content.map.image, alt: content.map.alt }))),
            React.createElement("section", { id: "social", "aria-labelledby": "social-title" },
                React.createElement(Heading, { id: "social" }, "\u041D\u0430\u0448\u0456 \u043F\u0435\u0440\u0435\u0432\u0430\u0433\u0438"),
                content.social.map((paragraph, index) => (React.createElement("p", { key: index }, paragraph)))),
            React.createElement(Feedback, null),
            React.createElement("section", { id: "faq", "aria-labelledby": "faq-title" },
                React.createElement(Heading, { id: "faq" }, "\u0427\u0430\u0441\u0442\u0456 \u0437\u0430\u043F\u0438\u0442\u0430\u043D\u043D\u044F"),
                React.createElement("div", { className: "faq-list" }, content.faq.map((question) => (React.createElement("details", { className: "faq-item", key: question.question },
                    React.createElement("summary", null, question.question),
                    React.createElement("div", { className: "faq-answer" },
                        React.createElement("p", null, question.answer))))))),
            React.createElement("section", { id: "reviews", "aria-labelledby": "reviews-title" },
                React.createElement(Heading, { id: "reviews" }, "\u0412\u0456\u0434\u0433\u0443\u043A\u0438 \u043A\u043B\u0456\u0454\u043D\u0442\u0456\u0432"),
                React.createElement("div", { className: "reviews-grid" }, content.reviews.map((review) => (React.createElement("article", { className: "review-card", key: review.name },
                    React.createElement("blockquote", null, review.quote),
                    React.createElement("figure", { className: "review-author" },
                        React.createElement("img", { className: "avatar", src: review.avatar, alt: `Аватар: ${review.name}` }),
                        React.createElement("figcaption", null,
                            React.createElement("span", { className: "review-name" }, review.name),
                            React.createElement("span", { className: "review-rating", "aria-label": `Оцінка ${review.rating} з 5` },
                                '★'.repeat(review.rating),
                                '☆'.repeat(5 - review.rating))))))))),
            React.createElement("section", { id: "team", "aria-labelledby": "team-title" },
                React.createElement(Heading, { id: "team" }, "\u041D\u0430\u0448\u0430 \u043A\u043E\u043C\u0430\u043D\u0434\u0430"),
                React.createElement("div", { className: "team-grid" }, content.team.map((member) => (React.createElement("article", { className: "team-card", key: member.name },
                    React.createElement("img", { className: "avatar", src: member.avatar, alt: `Аватар: ${member.name}` }),
                    React.createElement("span", { className: "team-badge" }, member.role),
                    React.createElement("h3", null, member.name),
                    React.createElement("p", null, member.description),
                    React.createElement(SocialLinks, { links: member.links })))))),
            React.createElement("section", { id: "history", "aria-labelledby": "history-title" },
                React.createElement(Heading, { id: "history" }, "\u0406\u0441\u0442\u043E\u0440\u0456\u044F \u0442\u0435\u043F\u043B\u0438\u0446\u0456"),
                React.createElement("p", { className: "section-intro" }, "\u042F\u043A \u0440\u043E\u0437\u0432\u0438\u0432\u0430\u0432\u0441\u044F \u00AB\u0417\u0435\u043B\u0435\u043D\u0438\u0439 \u0414\u0456\u043C\u00BB - \u0432\u0456\u0434 \u0456\u0434\u0435\u0457 \u0434\u043E \u043F\u0435\u0440\u0448\u043E\u0433\u043E \u0432\u0440\u043E\u0436\u0430\u044E."),
                React.createElement("div", { className: "timeline" }, content.history.map((item) => (React.createElement("article", { className: "timeline-item", key: item.year },
                    React.createElement("time", { className: "timeline-year", dateTime: item.year }, item.year),
                    React.createElement("h3", null, item.title),
                    React.createElement("p", null, item.description))))))),
        React.createElement("aside", { className: "ad" },
            React.createElement("p", { className: "ad_text text_border" }, content.adTitle)),
        React.createElement("footer", { className: "footer" },
            React.createElement(SocialLinks, { links: content.socialLinks }),
            React.createElement("p", null, content.footer))));
}
