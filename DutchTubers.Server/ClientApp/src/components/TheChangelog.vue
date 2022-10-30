<template>
    <ul>
        <li>
            <h3 class="text-lg text-gray-400">30-10-2022</h3>
            <ul class="pl-4 list-disc">
                <li>Rewrote the server application entirely so the site no longer needs to do two queries to get all data. The list of VTubers still remains ordered so that whomever is live will appear first, but besides that the order is no longer alphabetically sorted by VTubers' names and just random so VTubers with names later in the alphabet will never appear at rock bottom of the page.</li>
                <li>Rewrote the entire site, now using Vue3 + TypeScript instead of Elm, making it lots easier to update the site.</li>
                <li>Redesigned the site, now using TailwindCSS as a CSS framework, allowing for easier (and better looking) site look &amp; feel.</li>
                <li>Added unit tests for the server application, increasing its reliability.</li>
            </ul>
        </li>
    </ul>
</template>