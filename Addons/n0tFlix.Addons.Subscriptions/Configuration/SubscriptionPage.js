define(["loading", "dialogHelper", "emby-checkbox", "emby-select", "emby-input"],
    function (loading, dialogHelper) {
        var pluginId = "FD09E4AD-B04E-4C19-A704-8D3ED6980CC0";
        var page;

        function openAddCsvDialog() {
            var dlg = dialogHelper.createDialog({
                size: "medium-tall",
                removeOnClose: !0,
                scrollY: !1
            });

            dlg.classList.add("formDialog");
            dlg.classList.add("ui-body-a");
            dlg.classList.add("background-theme-a");
            dlg.classList.add("newSubscription");
            dlg.style = "max-width:65%;";

            var html = '';
            html += '<div class="formDialogHeader" style="display:flex">';
            html += '<button is="paper-icon-button-light" class="btnCloseDialog autoSize paper-icon-button-light" tabindex="-1"><i class="md-icon"></i></button><h3 id="headerContent" class="formDialogHeaderTitle">Subscription</h3>';
            html += '</div>';

            html += '<div class="formDialogContent" style="margin:2em;>';
            html += '<div class="dialogContentInner dialog-content-centered scrollY" style="height:25em;">';

            html += '<div class="inputContainer">';
            html += '<label class="inputLabel inputLabelUnfocused" for="csvFileLocation">Csv file location</label>';
            html += '<input is="emby-input" type="text" name="csvFileLocation" id="csvFileLocation" label="CSV File Location" class="emby-input"> ';
            html += '<div class="fieldDescription">The location of the csv file.</div>';
            html += '</div>';

            html += '<div class="formDialogFooter" style="padding-top:2em">';
            html += '<button id="addCsv" style="width: 50%;" is="emby-button" type="submit" class="raised button-submit block formDialogFooterItem emby-button">Add csv</button>';
            html += '</div>';

            html += '</div>';
            html += '</div>';

            dlg.innerHTML = html;
            dialogHelper.open(dlg);

            dlg.querySelector('#addCsv').addEventListener('click',
                () => {
                    var csvDirectoryLocation = dlg.querySelector('#csvFileLocation').value;

                    ApiClient.getJSON(ApiClient.getUrl("AddCsvInfo?Path=" + csvDirectoryLocation)).then(
                        (result) => {
                            if (result.response == "OK") {
                                ApiClient.getPluginConfiguration(pluginId).then((config) => {
                                    loadPageData(config);
                                    ApiClient.updatePluginConfiguration(pluginId, config).then(r => {
                                        Dashboard.processPluginConfigurationUpdateResult(r);
                                        dialogHelper.close(dlg);
                                    });
                                });
                            }
                        });
                });

            dlg.querySelector('.btnCloseDialog').addEventListener('click', () => {
                dialogHelper.close(dlg);
            });
        }

        function openEmailSettingsDialog(config) {
            var dlg = dialogHelper.createDialog({
                size: "medium-tall",
                removeOnClose: !0,
                scrollY: !1
            });

            dlg.classList.add("formDialog");
            dlg.classList.add("ui-body-a");
            dlg.classList.add("background-theme-a");
            dlg.classList.add("email");
            dlg.style = "max-width:65%;";

            var html = '';
            html += '<div class="formDialogHeader" style="display:flex">';
            html += '<button is="paper-icon-button-light" class="btnCloseDialog autoSize paper-icon-button-light" tabindex="-1"><i class="md-icon"></i></button><h3 id="headerContent" class="formDialogHeaderTitle">Email</h3>';
            html += '</div>';

            html += '<div class="formDialogContent" style="margin:2em;">';
            html += '<div class="dialogContentInner dialog-content-centered scrollY" style="height:35em;">';

            html += '<div class="inputContainer">';
            html += '<label class="inputLabel inputLabelUnfocused" for="companyName">Company name</label>';
            html += '<input is="emby-input" type="text" name="companyName" id="companyName" label="Company Name" class="emby-input"> ';
            html += '<div class="fieldDescription">The of the company sending the email</div>';
            html += '</div>';

            html += '<div class="inputContainer">';
            html += '<label class="inputLabel inputLabelUnfocused" for="smtpEmail">SMTP host</label>';
            html += '<input is="emby-input" type="text" name="smtpEmail" id="smtpEmail" label="SMTP host" class="emby-input"> ';
            html += '<div class="fieldDescription">The smtp host email: example smtp.gmail.com</div>';
            html += '</div>';

            html += '<div class="inputContainer">';
            html += '<label class="inputLabel inputLabelUnfocused" for="smtpPort">SMTP port</label>';
            html += '<input is="emby-input" type="number" name="smtpPort" id="smtpPort" label="SMTP port" class="emby-input"> ';
            html += '<div class="fieldDescription">The smtp port</div>';
            html += '</div>';

            html += '<div class="inputContainer">';
            html += '<label class="inputLabel inputLabelUnfocused" for="senderEmailAddress">Sender Email Address</label>';
            html += '<input is="emby-input" type="text" name="senderEmailAddress" id="senderEmailAddress" label="Sender Email Address" class="emby-input"> ';
            html += '<div class="fieldDescription">The sender email: example@gmail.com</div>';
            html += '</div>';

            html += '<div class="inputContainer">';
            html += '<label class="inputLabel inputLabelUnfocused" for="emailUserName">Email user name</label>';
            html += '<input is="emby-input" type="text" name="emailUserName" id="emailUserName" label="Email user name" class="emby-input"> ';
            html += '<div class="fieldDescription">The Email login user name</div>';
            html += '</div>';

            html += '<div class="inputContainer">';
            html += '<label class="inputLabel inputLabelUnfocused" for="emailPassword">Email password</label>';
            html += '<input is="emby-input" type="password" name="emailPassword" id="emailPassword" label="Email password" class="emby-input"> ';
            html += '<div class="fieldDescription">The Email login password</div>';
            html += '</div>';

            html += '</div>';

            html += '<div class="formDialogFooter" style="padding-top:2em">';
            html += '<button id="saveEmail" style="width: 50%;" is="emby-button" type="submit" class="raised button-submit block formDialogFooterItem emby-button">Ok</button>';
            html += '</div>';

            html += '</div>';

            dlg.innerHTML = html;
            dialogHelper.open(dlg);

            if (config.hostSmtpInformation) {
                dlg.querySelector('#smtpEmail').value = config.hostSmtpInformation.smtpHost;
                dlg.querySelector('#senderEmailAddress').value = config.hostSmtpInformation.senderAddress;
                dlg.querySelector('#smtpPort').value = config.hostSmtpInformation.smtpPort;
                dlg.querySelector('#senderEmailAddress').value = config.hostSmtpInformation.senderAddress;
                dlg.querySelector('#emailUserName').value = config.hostSmtpInformation.emailUserName;
                dlg.querySelector('#emailPassword').value = config.hostSmtpInformation.emailPassword;
                dlg.querySelector('#companyName').value = config.hostSmtpInformation.emailDisplayName;
            }
            dlg.querySelector('.btnCloseDialog').addEventListener('click', () => {
                dialogHelper.close(dlg);
            });

            dlg.querySelector('#saveEmail').addEventListener('click', () => {
                var emailUpdate = {
                    smtpHost: dlg.querySelector('#smtpEmail').value,
                    senderAddress: dlg.querySelector('#senderEmailAddress').value,
                    smtpPort: dlg.querySelector('#smtpPort').value,
                    emailUserName: dlg.querySelector('#emailUserName').value,
                    emailPassword: dlg.querySelector('#emailPassword').value,
                    emailDisplayName: dlg.querySelector('#companyName').value
                }
                config.hostSmtpInformation = emailUpdate;
                ApiClient.updatePluginConfiguration(pluginId, config).then(result => {
                    Dashboard.processPluginConfigurationUpdateResult(result);
                    dialogHelper.close(dlg);
                });
            });
        }

        function openEditSubscriptionDialog(config, isNewSubscription, subscriptionId) {
            var dlg = dialogHelper.createDialog({
                size: "medium-tall",
                removeOnClose: !0,
                scrollY: !1
            });

            dlg.classList.add("formDialog");
            dlg.classList.add("ui-body-a");
            dlg.classList.add("background-theme-a");
            dlg.classList.add("newSubscription");
            dlg.style = "max-width:65%;";

            ApiClient.getJSON(ApiClient.getUrl("Users")).then(users => {
                var html = '';
                html += '<div class="formDialogHeader" style="display:flex">';
                html += '<button is="paper-icon-button-light" class="btnCloseDialog autoSize paper-icon-button-light" tabindex="-1"><i class="md-icon"></i></button><h3 id="headerContent" class="formDialogHeaderTitle">Subscription</h3>';
                html += '</div>';

                html += '<div class="formDialogContent" style="margin:2em;>';
                html += '<div class="dialogContentInner dialog-content-centered scrollY" style="height:25em;">';

                html += '<div class="infoBanner restartInfoBanner flex align-items-center">';
                html += 'Create the new User for Emby before creating the Subscription';
                html += '</div>';

                html += '<div class="selectContainer">';
                html += '<label for="userName"  class="selectLabel"></label>';
                html += '<select is="emby-input" data-mini="true"  class="emby-select-withcolor emby-select" name="userName" id="userName">';
                users.forEach(user => {
                    html += '<option value="' + user.Id + '">' + user.Name + '</option>';
                });
                html += '</select>';

                html += '<div class="fieldDescription">Choose the User Profile to create the subscription for.</div>';
                html += '<div class="selectArrowContainer">';
                html += '<div style="visibility:hidden;">0</div>';
                html += '<i class="selectArrow md-icon"></i>';
                html += '</div>';
                html += '</div>';

                html += '<div class="inputContainer">';
                html += '<label class="inputLabel inputLabelUnfocused" for="subscriptionEmail">Subscription Email</label>';
                html += '<input is="emby-input" type="text" name="subscriptionEmail" id="subscriptionEmail" label="Subscription Email" class="emby-input"> ';
                html += '<div class="fieldDescription">The new subscription email address.</div>';
                html += '</div>';

                html += '<div class="inputContainer">';
                html += '<label class="inputLabel inputLabelUnfocused" for="subscriptionStartDate">Subscription Start Date</label>';
                html += '<input is="emby-input" type="date" name="subscriptionStartDate" id="subscriptionStartDate" label="Subscription Start Date" class="emby-input"> ';
                html += '<div class="fieldDescription">The date the subscription starts.</div>';
                html += '</div>';

                html += '<div class="inputContainer">';
                html += '<label class="inputLabel inputLabelUnfocused" for="subscriptionEndDate">Subscription End Date</label>';
                html += '<input is="emby-input" type="date" name="subscriptionEndDate" id="subscriptionEndDate" label="Subscription End Date" class="emby-input"> ';
                html += '<div class="fieldDescription">The date the subscription ends.</div>';
                html += '</div>';

                html += '</div>';

                html += '<div class="formDialogFooter" style="padding-top:2em">';
                html += '<button id="saveSubscription" style="width: 50%;" is="emby-button" type="submit" class="raised button-submit block formDialogFooterItem emby-button">Save Subscription</button>';
                html += '</div>';

                html += '</div>';

                dlg.innerHTML = html;
                dialogHelper.open(dlg);

                if (!isNewSubscription) {
                    config.subscriptions.forEach(subscription => {
                        if (subscription.Id == subscriptionId) {
                            dlg.querySelector('#userName').value = subscription.user.Id;
                            dlg.querySelector('#subscriptionEmail').value = subscription.email;
                            dlg.querySelector('#subscriptionStartDate').value = subscription.subscriptionStart;
                            dlg.querySelector('#subscriptionEndDate').value = subscription.subscriptionExpire;
                            if (subscription.flagForRenewal) {
                                dlg.querySelector('#headerContent').value = "Subscription flagged for renewal";
                            }
                        }
                    });
                } else {
                    dlg.querySelector('#subscriptionStartDate').value = new Date(Date.now()).toISOString().slice(0, 10);
                }

                dlg.querySelector('.btnCloseDialog').addEventListener('click', () => {
                    dialogHelper.close(dlg);
                });

                dlg.querySelector('#saveSubscription').addEventListener('click', () => {
                    users.forEach(u => {
                        var userNameSelect = dlg.querySelector('#userName');
                        if (u.Id == userNameSelect.options[userNameSelect.selectedIndex >= 0 ? userNameSelect.selectedIndex : 0].value) {
                            var update = {
                                user: u,
                                validSubscription: true,
                                email: dlg.querySelector('#subscriptionEmail').value,
                                subscriptionStart: new Date(dlg.querySelector('#subscriptionStartDate').value).toISOString().slice(0, 10),
                                subscriptionExpire: new Date(dlg.querySelector('#subscriptionEndDate').value).toISOString().slice(0, 10),
                                Id: u.Id
                            }
                            var newSubscriptions = [];

                            config.subscriptions.forEach(subscription => {
                                if (subscription.Id != u.Id) {
                                    newSubscriptions.push(subscription);
                                }
                            });

                            newSubscriptions.push(update);

                            config.subscriptions = newSubscriptions;

                            ApiClient.updatePluginConfiguration(pluginId, config).then(result => {
                                Dashboard.processPluginConfigurationUpdateResult(result);
                                loadPageData(config);
                                dialogHelper.close(dlg);
                            });
                        }
                    });
                });
            });
        }

        function getSubscriptionTableItemsHtml(subscription) {
            var color = "#000";
            if (subscription.flagForRenewal) {
                color = "gold";
            }
            if (subscription.validSubscription == false) {
                color = "orangered";
            }
            var html = '';
            html += '<tr style="color:' + color + ' " id="' + subscription.Id + '">';
            html += '<td data-title="Name" class="detailTableBodyCell fileCell">' + subscription.user.Name + '</td>';
            html += '<td data-title="Valid Account" class="detailTableBodyCell fileCell">' + subscription.validSubscription + '</td>';
            if (subscription.validSubscription == false) {
                var expire = new Date(subscription.subscriptionExpire);
                var accountRemoval = new Date(expire.setDate(expire.getDate() + 30));
                var now = new Date();
                // To calculate the time difference of two dates
                var Difference_In_Time = accountRemoval.getTime() - now;

                // To calculate the no. of days between two dates
                var Difference_In_Days = Math.round(Difference_In_Time / (1000 * 3600 * 24));

                html += '<td data-title="Days Until Removal" class="detailTableBodyCell fileCell">' +
                    (Difference_In_Days) +
                    '</td>';
            } else {
                html += '<td data-title="Days Until Removal" class="detailTableBodyCell fileCell"></td>';
            }

            html += '<td data-title="Account Id" class="detailTableBodyCell fileCell">' + subscription.Id + '</td>';
            html += '<td data-title="Start Date" class="detailTableBodyCell fileCell">' + subscription.subscriptionStart + '</td>';
            html += '<td data-title="Expire Date" class="detailTableBodyCell fileCell">' + subscription.subscriptionExpire + '</td>';
            html += '<td data-title="Email Address" class="detailTableBodyCell fileCell">' + subscription.email + '</td>';
            if (subscription.reminderSent) {
                if (subscription.reminderSent == true) {
                    html +=
                        '<td data-title="Reminder Email Sent" class="detailTableBodyCell fileCell">Reminder Sent</td>';
                } else {
                    html +=
                        '<td data-title="Reminder Email Sent" class="detailTableBodyCell fileCell">No Reminder Email Sent</td>';
                }
            } else {
                html +=
                    '<td data-title="Reminder Email Sent" class="detailTableBodyCell fileCell"></td>';
            }
            html += '<td data-title="Edit" class="detailTableBodyCell fileCell"><button is="paper-icon-button-light" style="margin-left:1em; color:#000" class="fab emby-input-iconbutton paper-icon-button-light subscriptionEdit" id="' + subscription.Id + '"><i class="md-icon">edit</i></button></td>';
            html += '<td data-title="Remove" class="detailTableBodyCell fileCell"><button is="paper-icon-button-light" style="margin-left:1em; color:#000" class="fab emby-input-iconbutton paper-icon-button-light subscriptionDelete" id="' + subscription.Id + '"><i class="md-icon">delete</i></button></td>';

            html += '</tr>';
            return html;
        }

        ApiClient.deleteUserResult = function (id) {
            var url = this.getUrl("Users/" + id);

            return this.ajax({
                type: "DELETE",
                url: url
            });
        };

        function loadPageData(config) {
            if (config.adminSubscriptionPass) {
                page.querySelector('#adminPass').value = config.adminSubscriptionPass;
            }
            if (config.subscriptions) {
                var subscriptionList = page.querySelector('#subscriptions');
                subscriptionList.innerHTML = '';

                config.subscriptions.forEach(subscription => {
                    subscriptionList.innerHTML += getSubscriptionTableItemsHtml(subscription);
                });

                page.querySelectorAll('.subscriptionDelete').forEach(button => {
                    button.addEventListener('click',
                        (e) => {
                            ApiClient.deleteUserResult(e.target.parentElement.id);

                            var newSubscriptions = [];
                            ApiClient.getPluginConfiguration(pluginId).then((c) => {
                                c.subscriptions.forEach(subscription => {
                                    if (e.target.parentElement.id != subscription.Id) {
                                        newSubscriptions.push(subscription);
                                    }
                                });
                                c.subscriptions = newSubscriptions;
                                ApiClient.updatePluginConfiguration(pluginId, c).then(result => {
                                    Dashboard.processPluginConfigurationUpdateResult(result);
                                    loadPageData(c);
                                });
                            });
                        });
                });

                page.querySelectorAll('.subscriptionEdit').forEach(button => {
                    button.addEventListener('click',
                        (e) => {
                            ApiClient.getPluginConfiguration(pluginId).then((c) => {
                                openEditSubscriptionDialog(c, false, e.target.parentElement.id);
                            });
                        });
                });
            }
        }

        function loadConfig() {
            ApiClient.getPluginConfiguration(pluginId).then(
                (config) => {
                    loadPageData(config);
                    loading.hide();
                });
        }

        return function (view) {
            page = view;
            view.addEventListener('viewshow',
                () => {
                    loading.show();
                    loadConfig();
                    view.querySelector('#addSubscription').addEventListener('click', () => {
                        ApiClient.getPluginConfiguration(pluginId).then((config) => {
                            openEditSubscriptionDialog(config, true, view);
                        });
                    });

                    view.querySelector('#emailSettingsDialog').addEventListener('click', () => {
                        ApiClient.getPluginConfiguration(pluginId).then((config) => {
                            openEmailSettingsDialog(config);
                        });
                    });
                    view.querySelector('#addCsvDialog').addEventListener('click', () => {
                        openAddCsvDialog();
                    });
                    view.querySelector('#saveAdminPass').addEventListener('click', () => {
                        var pass = view.querySelector('#adminPass').value;
                        ApiClient.getPluginConfiguration(pluginId).then((config) => {
                            config.adminSubscriptionPass = pass;
                            ApiClient.updatePluginConfiguration(pluginId, config).then(result => {
                                Dashboard.processPluginConfigurationUpdateResult(result);
                            });
                        });
                    });
                });
        }
    });