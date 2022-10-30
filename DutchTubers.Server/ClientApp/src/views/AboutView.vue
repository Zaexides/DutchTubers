<script setup lang="ts">
    import ContentPanel from "../components/ContentPanel.vue";
    import TheChangelog from "../components/TheChangelog.vue";
</script>

<template>
    <div class="grid grid-cols-1 gap-4 md:grid-cols-4">
        <div class="space-y-4 order-last md:col-span-3 md:order-first">
            <ContentPanel title="About" id="about">
                <p>
                    DutchTubers is a website that lists all Dutch VTubers, created by Zaexides and inspired by divusmetaphysica and Mariel_VT's <a href="https://www.finntubers.fi/" class="underline hover:text-orange-700">finntubers.fi</a> website.
                </p>
            </ContentPanel>
            <ContentPanel title="FAQ" id="faq">
                <div class="space-y-6">
                    <div class="space-y-2">
                        <h3 id="faq-want-to-be-on-site" class="text-lg text-gray-400">I am a Dutch VTuber and want to be on this site</h3>
                        <p>
                            Right now there's no integrated way of applying for a position on the website. This is something I am planning on adding as a feature later down the line. Please keep in mind this site is a pet project of mine and I cannot always work on it. The best way to apply right now is by contacting me.
                        </p>
                    </div>
                    <div class="space-y-2">
                        <h3 id="faq-requirements" class="text-lg text-gray-400">What are the requirements for appearing on the site?</h3>
                        <p>
                            There are currently no conditions set in stone, but in general I check for these conditions:
                        </p>
                        <ul class="pl-4 list-disc">
                            <li>You need to be Dutch, either as a current or a former resident of the Kingdom of the Netherlands.</li>
                            <li>You need to stream regularly (about once a week should suffice)</li>
                            <li>You need to have been an active streamer for a while now (at least 1-2 months)</li>
                        </ul>
                        <p>
                            Exceptions to these conditions may be made if an argument can be provided for it.
                        </p>
                    </div>
                    <div class="space-y-2">
                        <h3 id="faq-youtube" class="text-lg text-gray-400">I stream on YouTube, do I have a place on this site?</h3>
                        <p>
                            Absolutely! Right now the site doesn't support YouTube's API, but this is planned for sometime in the future.
                        </p>
                    </div>
                </div>
            </ContentPanel>
            <ContentPanel title="Contact" id="contact">
                <p>
                    You can contact me with any questions in a variety of ways:
                </p>
                <ul>
                    <li>Twitter: <a href="https://twitter.com/zaexides" class="underline hover:text-orange-700">@Zaexides</a></li>
                    <li>Twitch: <a href="https://www.twitch.tv/zaexides" class="underline hover:text-orange-700">Zaexides</a></li>
                    <li>Email: <a href="mailto:zaexides@zaexides.net" class="underline hover:text-orange-700">zaexides@zaexides.net</a></li>
                </ul>
            </ContentPanel>
            <ContentPanel title="Latest Updates" id="changelog">
                <TheChangelog />
            </ContentPanel>
        </div>

        <div>
            <ContentPanel title="Quick Navigation" cssClass="md:sticky md:top-0">
                <nav>
                    <ul class="space-y-2 md:space-y-0">
                        <li><a href="#about" class="flex space-x-2"><div class="text-stone-500">#</div><div>About</div></a></li>
                        <li>
                            <a href="#faq" class="flex space-x-2"><div class="text-stone-500">#</div><div>FAQ</div></a>
                            <ul class="pl-4 space-y-2 md:space-y-0">
                                <li>
                                    <a href="#faq-want-to-be-on-site" class="flex space-x-2">
                                        <div class="text-stone-500">#</div>
                                        <div>I am a Dutch VTuber and want to be on this site</div>
                                    </a>
                                </li>
                                <li>
                                    <a href="#faq-requirements" class="flex space-x-2">
                                        <div class="text-stone-500">#</div>
                                        <div>What are the requirements for appearing on the site?</div>
                                    </a>
                                </li>
                                <li>
                                    <a href="#faq-youtube" class="flex space-x-2">
                                        <div class="text-stone-500">#</div>
                                        <div>I stream on YouTube, do I have a place on this site?</div>
                                    </a>
                                </li>
                            </ul>
                        </li>
                        <li><a href="#contact" class="flex space-x-2"><div class="text-stone-500">#</div><div>Contact</div></a></li>
                        <li><a href="#changelog" class="flex space-x-2"><div class="text-stone-500">#</div><div>Latest Updates</div></a></li>
                    </ul>
                </nav>
            </ContentPanel>
        </div>
    </div>
</template>